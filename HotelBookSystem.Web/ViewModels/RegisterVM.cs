using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookSystem.Web.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Пароль")]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public required string ConfirmPassword { get; set; }
        [Required]
        [Display(Name="Имя")]
        public required string Name { get; set; }
        [Display(Name="Номер телефона")]
        public string? PhoneNumber { get; set; }
        public string? RedirectURL { get; set; }
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
