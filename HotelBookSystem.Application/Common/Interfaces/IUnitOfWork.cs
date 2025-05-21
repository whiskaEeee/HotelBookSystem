using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookSystem.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IHotelRepository Hotel { get; }
        IHotelNumberRepository HotelNumber { get; }
        IAmenityRepository Amenity { get; }
        IBookingRepository Booking { get; }
        IApplicationUserRepository User { get; }
        void Save();
    }
}
