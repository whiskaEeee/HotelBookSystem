using System.Diagnostics;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Application.Utility;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Infrastructure.Data;
using HotelBookSystem.Web.Models;
using HotelBookSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace HotelBookSystem.Web.Controllers
{
    [Authorize(Roles = StaticDetails.AdminRole)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Hotel");
            return View(amenities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                if (amenityVM.Amenity is not null)
                {
                    _unitOfWork.Amenity.Add(amenityVM.Amenity);
                    _unitOfWork.Save();
                    TempData["success"] = "Успешно добавлено";
                    return RedirectToAction(nameof(Index));
                }
            }

            amenityVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(amenityVM);
        }

        [HttpGet]
        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity is null)
            {
                RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                if (amenityVM.Amenity is not null)
                {
                    _unitOfWork.Amenity.Update(amenityVM.Amenity);
                    _unitOfWork.Save();
                    TempData["success"] = "Успешно обновлено";
                    return RedirectToAction(nameof(Index));
                }
            }

            amenityVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(amenityVM);
        }

        [HttpGet]
        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };

            if (amenityVM.Amenity is null)
            {
                RedirectToAction("Error", "Home");
            }

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            if (amenityVM.Amenity is not null)
            {
                Amenity? objFromDb = _unitOfWork.Amenity
                    .Get(u => u.Id == amenityVM.Amenity.Id);

                if (objFromDb is not null)
                {
                    _unitOfWork.Amenity.Remove(objFromDb);
                    _unitOfWork.Save();
                    TempData["success"] = "Успешно удалено";
                    return RedirectToAction("Index");
                }
            }

            return View(amenityVM);
        }
    }
}
