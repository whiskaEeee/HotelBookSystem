using System.Diagnostics;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Application.Utility;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Infrastructure.Data;
using HotelBookSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HotelBookSystem.Web.Controllers
{
    [Authorize(Roles = StaticDetails.AdminRole)]
    public class HotelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageValidator _imageValidator;

        public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IImageValidator imageValidator)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imageValidator = imageValidator;
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

            _imageValidator.Validate(obj.Image, ModelState);

            if (ModelState.IsValid)
            {
                if(obj.Image is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\HotelImages");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUrl = @"\images\HotelImages\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }


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

            _imageValidator.Validate(obj.Image, ModelState);

            if (ModelState.IsValid && obj.Id > 0)
            {
                if (obj.Image is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\HotelImages");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUrl = @"\images\HotelImages\" + fileName;
                }

                _unitOfWork.Hotel.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Успешно обновлено";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
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
                if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.Hotel.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Успешно удалено";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
    }
}
