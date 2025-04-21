using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public double? Price { get; set; }
        [Range(1, 3)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        public DateOnly Last_Update { get; set; }


    }
}
