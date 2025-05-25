using System.Security.Claims;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Application.Utility;
using HotelBookSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace HotelBookSystem.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult FinalizeBooking(int hotelId, DateOnly checkInDate, int numberOfNights)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId);

            Booking booking = new()
            {
                HotelId = hotelId,
                Hotel = _unitOfWork.Hotel.Get(u => u.Id == hotelId, includeProperties: "Amenities"),
                CheckInDate = checkInDate,
                CheckOutDate = checkInDate.AddDays(numberOfNights),
                NumberOfNights = numberOfNights,
                UserId = userId ?? "",
                Name = user.Name,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber,
            };
            booking.TotalCost = booking.Hotel.Price * numberOfNights;
            return View(booking);
        }

        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            if (string.IsNullOrEmpty(booking.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Введите номер телефона");
            }
            if (ModelState.IsValid)
            {
                var hotel = _unitOfWork.Hotel.Get(u => u.Id == booking.HotelId);

                booking.TotalCost = hotel.Price * booking.NumberOfNights;
                booking.Status = StaticDetails.StatusPending;
                booking.BookingDate = DateOnly.FromDateTime(DateTime.Now);

                _unitOfWork.Booking.Add(booking);
                _unitOfWork.Save();

                var domain = Request.Scheme + "://" + Request.Host.Value;
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),

                    Mode = "payment",
                    SuccessUrl = domain + $"/booking/BookingConfirmation?bookingId={booking.Id}",
                    CancelUrl = domain + $"/booking/FinalizeBooking?hotelId={booking.HotelId}&" +
                    $"checkInDate={booking.CheckInDate}&numberOfNights={booking.NumberOfNights}",
                };

                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "rub",
                        UnitAmount = (long)(booking.TotalCost * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = booking.Name,

                        }
                    },
                    Quantity = 1
                });

                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Append("Location", session.Url);

                _unitOfWork.Booking.UpdateStripePaymentId(booking.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();

                return new StatusCodeResult(303);
            }
            booking.Hotel = _unitOfWork.Hotel.Get(u => u.Id == booking.HotelId, includeProperties: "Amenities");
            booking.TotalCost = booking.Hotel.Price * booking.NumberOfNights;
            booking.User = _unitOfWork.User.Get(u => u.Id == booking.UserId);
            return View(booking);
        }

        [HttpGet]
        public IActionResult BookingConfirmation(int bookingId)
        {
            Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId,
                includeProperties: "User,Hotel");

            if (string.Equals(bookingFromDb.Status, StaticDetails.StatusPending))
            {
                var service = new SessionService();
                Session session = service.Get(bookingFromDb.StripeSessionId);

                if (string.Equals(session.PaymentStatus, "paid"))
                {
                    _unitOfWork.Booking.UpdateStatus(bookingId, StaticDetails.StatusApproved);
                    _unitOfWork.Booking.UpdateStripePaymentId(bookingId, session.Id, session.PaymentIntentId);
                    _unitOfWork.Save();
                }
            }
            return View(bookingId);
        }

        [HttpGet]
        public IActionResult BookingDetails(int bookingId)
        {
            Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId,
                includeProperties: "User,Hotel");
            if (bookingFromDb is null)
            {
                return NotFound();
            }
            return View(bookingFromDb);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<string> statusList = new()
            {
                StaticDetails.StatusPending,
                StaticDetails.StatusApproved,
                StaticDetails.StatusCheckedIn,
                StaticDetails.StatusCompleted,
                StaticDetails.StatusCancelled,
                StaticDetails.StatusRefunded
            };
            return View(statusList);
        }

        // ==================================== API CALLS ====================================
        #region API Calls
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<Booking> bookings;

            if (User.IsInRole(StaticDetails.AdminRole))
            {
                bookings = _unitOfWork.Booking.GetAll(includeProperties: "User,Hotel");
            }
            else
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId is null) 
                {
                    return Json(new { data = Enumerable.Empty<Booking>() });
                }

                bookings = _unitOfWork.Booking.GetAll(u => u.UserId == userId, includeProperties: "User,Hotel");
            }
            if (!string.IsNullOrEmpty(status))
            {
                bookings = bookings.Where(u => u.Status != null && u.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }
            return Json(new { data = bookings });
        }
        #endregion
    }
}