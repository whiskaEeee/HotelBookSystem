using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookSystem.Domain.Entities;

namespace HotelBookSystem.Application.Utility
{
    public static class StaticDetails
    {
        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusCheckedIn = "CheckedIn";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public static int CountAvaliableRooms(int hotelId,
            List<HotelNumber> hotelNumbersList, DateOnly checkInDate, int numberOfNights,
            List<Booking> bookings)
        {
            List<int> bookingInDate = new();
            int roomsInHotel = hotelNumbersList.Where(h => h.HotelId == hotelId).Count();
            int finalAvaliableRoomsCount = int.MaxValue;

            for (int i = 0; i < numberOfNights; i++)
            {
                // все бронирования в отеле на этот день
                var bookingsInOneDate = bookings.Where(b => b.HotelId == hotelId
                    && b.CheckInDate <= checkInDate.AddDays(i)
                    && b.CheckOutDate > checkInDate.AddDays(i));

                foreach (var booking in bookingsInOneDate)
                {
                    if (!bookingInDate.Contains(booking.Id))
                    {
                        //список всех бронирований за период
                        bookingInDate.Add(booking.Id);
                    }
                }
                var totalAvaliableRooms = roomsInHotel - bookingInDate.Count;
                if(totalAvaliableRooms <= 0)
                {
                    return 0;
                }
                else if (totalAvaliableRooms < finalAvaliableRoomsCount)
                {
                    finalAvaliableRoomsCount = totalAvaliableRooms;
                }
            }
            return finalAvaliableRoomsCount;
        }
    }
}
