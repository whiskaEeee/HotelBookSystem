using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookSystem.Application.Common.Interfaces
{
    public interface IImageValidator
    {
        public void Validate(IFormFile? image, ModelStateDictionary modelState);
    }
}
