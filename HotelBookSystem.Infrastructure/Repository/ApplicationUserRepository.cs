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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
