﻿using System.Diagnostics;
using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Application.Utility;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Infrastructure.Data;
using HotelBookSystem.Infrastructure.Repository;
using HotelBookSystem.Web.Models;
using HotelBookSystem.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBookSystem.Web.Controllers
{
    [Authorize(Roles = StaticDetails.AdminRole)]
    public class HotelNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var hotelNumbers = _unitOfWork.HotelNumber.GetAll(includeProperties: "Hotel"); 
            return View(hotelNumbers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Create(HotelNumberVM hotelNumberVM)
        {
            if (hotelNumberVM.HotelNumber is null)
            {
                hotelNumberVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(hotelNumberVM);
            }

            bool roomNumberExists = _unitOfWork.HotelNumber.Any(u => u.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.HotelNumber.Add(hotelNumberVM.HotelNumber);
                _unitOfWork.Save();
                TempData["success"] = "Успешно создано";
                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExists)
            {
                TempData["error"] = "Такая комната уже существует";
            }

            hotelNumberVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(hotelNumberVM);
        }

        [HttpGet]
        public IActionResult Update(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                HotelNumber = _unitOfWork.HotelNumber.Get(u => u.Hotel_Number == hotelNumberId)
            };

            if (hotelNumberVM.HotelNumber is null)
            {
                RedirectToAction("Error", "Home");
            }

            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Update(HotelNumberVM hotelNumberVM)
        {
            if (ModelState.IsValid)
            {
                if (hotelNumberVM.HotelNumber is not null)
                {
                    _unitOfWork.HotelNumber.Update(hotelNumberVM.HotelNumber);
                    _unitOfWork.Save();
                    TempData["success"] = "Успешно сохранено";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Ошибка: номер отеля не найден.";
                }
            }

            hotelNumberVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(hotelNumberVM);
        }

        [HttpGet]
        public IActionResult Delete(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                HotelNumber = _unitOfWork.HotelNumber.Get(u => u.Hotel_Number == hotelNumberId)
            };

            if (hotelNumberVM.HotelNumber is null)
            {
                RedirectToAction("Error", "Home");
            }

            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(HotelNumberVM hotelNumberVM)
        {
            HotelNumber? objFromDb = _unitOfWork.HotelNumber
                .Get(u => u.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);

            if (objFromDb is not null)
            {
                _unitOfWork.HotelNumber.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Успешно удалено";
                return RedirectToAction("Index");
            }

            return View(hotelNumberVM);
        }

    }
}
