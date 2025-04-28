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

        public HotelController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Hotels = _db.Hotels.ToList();
            return View(Hotels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(Hotel obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "Описание должно отличаться от имени");
            }
            if (ModelState.IsValid)
            {
                obj.Last_Update = DateOnly.FromDateTime(DateTime.Now);
                _db.Hotels.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Успешно создано";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int hotelId)
        {
            Hotel? item = _db.Hotels.FirstOrDefault(h => h.Id == hotelId);
            if (item is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Hotel obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "Описание должно отличаться от имени");
            }
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Hotels.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Успешно обновлено";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int hotelId)
        {
            Hotel? obj = _db.Hotels.FirstOrDefault(u => u.Id == hotelId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Hotel obj)
        {
            Hotel? objFromDb = _db.Hotels.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _db.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "Успешно удалено";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
