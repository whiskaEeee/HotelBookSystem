using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookSystem.Infrastructure.Repository
{
    public class HotelNumberRepository : Repository<HotelNumber>, IHotelNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(HotelNumber entity)
        {
            _db.HotelNumbers.Update(entity);
        }
    }
}
