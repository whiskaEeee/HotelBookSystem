using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookSystem.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        [ValidateNever]
        public Hotel Hotel { get; set; }
    }
}
