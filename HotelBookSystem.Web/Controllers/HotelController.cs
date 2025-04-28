using System.Diagnostics;
using HotelBookSystem.Domain.Entities;
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

        [HttpGet]
        public IActionResult Update(int Id)
        {
            Hotel? item = _db.Hotels.FirstOrDefault(h => h.Id == Id);
            if (item is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Hotel obj)
        {
            if(obj is not null)
            {
                if (ModelState.IsValid && obj.Id > 0)
                {
                    _db.Hotels.Update(obj);

                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }

    }
}
