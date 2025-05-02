using System.Diagnostics;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Infrastructure.Data;
using HotelBookSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookSystem.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Hotels = _unitOfWork.Hotel.GetAll();
            return View(Hotels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hotel obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "Описание должно отличаться от имени");
            }
            if (ModelState.IsValid)
            {
                obj.Last_Update = DateOnly.FromDateTime(DateTime.Now);
                _unitOfWork.Hotel.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Успешно создано";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int hotelId)
        {
            Hotel? item = _unitOfWork.Hotel.Get(u => u.Id == hotelId);
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
                _unitOfWork.Hotel.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Успешно обновлено";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int hotelId)
        {
            Hotel? obj = _unitOfWork.Hotel.Get(u => u.Id == hotelId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Hotel obj)
        {
            Hotel? objFromDb = _unitOfWork.Hotel.Get(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _unitOfWork.Hotel.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Успешно удалено";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
