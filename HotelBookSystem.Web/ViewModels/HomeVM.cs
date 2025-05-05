using HotelBookSystem.Domain.Entities;

namespace HotelBookSystem.Web.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Hotel>? Hotels { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }

    }
}
