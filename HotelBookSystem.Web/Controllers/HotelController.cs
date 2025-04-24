using System.Diagnostics;
using HotelBookSystem.Infrastructure.Data;
using HotelBookSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookSystem.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelController(ApplicationDbContext db  )
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var Hotels = _db.Hotels.ToList();
            return View(Hotels);
        }

    }
}
