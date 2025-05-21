using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HotelBookSystem.Domain.Entities
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public required string Name { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        [Display(Name = "Цена за ночь")]
        [Required]
        public required double Price { get; set; }
        [Range(1, 30)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        public DateOnly? Create_Date { get; set; }
        public DateOnly? Update_Date { get; set; }
        [ValidateNever]
        public List<Amenity> Amenities { get; set; }
        [NotMapped]
        public bool IsAvaliable { get; set; } = true;
    }
}
