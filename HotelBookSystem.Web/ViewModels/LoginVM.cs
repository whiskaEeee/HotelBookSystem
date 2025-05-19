using System.ComponentModel.DataAnnotations;

namespace HotelBookSystem.Web.ViewModels
{
    public class LoginVM
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? RedirectURL { get; set; }
    }
}
