using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HotelBookSystem.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? User { get; set; }
        [Required]
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        [ValidateNever]
        public Hotel? Hotel { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [Display(Name = "Номер телефона")]
        public string? PhoneNumber { get; set; }
        [Required]
        public double TotalCost { get; set; }
        public int NumberOfNights { get; set; }
        public string? Status { get; set; }

        [Required]
        public DateOnly BookingDate { get; set; }
        [Required]
        [Display(Name = "Дата заселения")]
        public DateOnly CheckInDate { get; set; }
        [Required]
        [Display(Name = "Дата окончания")]
        public DateOnly CheckOutDate { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }

        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }

        public int HotelNumber { get; set; }
    }
}
