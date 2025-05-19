using System.Diagnostics;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Web.Models;
using HotelBookSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Hotels = _unitOfWork.Hotel.GetAll(includeProperties: "Amenities"),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                NumberOfNights = 1
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetHotelsByDate(int NumberOfNights, DateOnly CheckInDate)
        {
            var Hotels = _unitOfWork.Hotel.GetAll(includeProperties: "Amenities");
            //симуляция логики доступности отелей
            foreach (var hotel in Hotels)
            {
                if (hotel.Id % 2 == 0)
                {
                    hotel.IsAvaliable = false;
                }
            }
            HomeVM homeVM = new()
            {
                Hotels = Hotels,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckInDate.AddDays(NumberOfNights),
                NumberOfNights = NumberOfNights
            };
            return PartialView("_HotelList", homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}