using HotelBookSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookSystem.Infrastructure.Repository
{
    public class ImageValidator : IImageValidator
    {
        private readonly List<string> allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
        private const long MaxFileSizeInBytes = 2 * 1024 * 1024;

        public void Validate(IFormFile? image, ModelStateDictionary modelState)
        {
            if (image is null || string.IsNullOrEmpty(image.FileName))
                return;
            
            if (!allowedExtensions.Contains(Path.GetExtension(image.FileName).ToLowerInvariant()))
                modelState.AddModelError("image", "Формат файла не подходит (png, jpg, jpeg)");

            if (image.Length > MaxFileSizeInBytes)
                modelState.AddModelError("image", "Размер файла не должен превышать 2 МБ");
        }
    }
}
