using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 2,
                    Name = "Seaside Escape",
                    Description = "A peaceful retreat by the ocean with stunning sunset views.",
                    Price = 5200,
                    Occupancy = 15,
                    ImageUrl = "https://placehold.co/600x402"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Mountain Lodge",
                    Description = "Cozy lodge located in the heart of the mountains, perfect for ski lovers.",
                    Price = 6100,
                    Occupancy = 10,
                    ImageUrl = "https://placehold.co/600x403"
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Urban Stay",
                    Description = "Modern hotel in the city center with quick access to all attractions.",
                    Price = 4500,
                    Occupancy = 40,
                    ImageUrl = "https://placehold.co/600x404"
                },
                new Hotel
                {
                    Id = 5,
                    Name = "Desert Oasis",
                    Description = "Luxurious desert resort with pools, spa, and unforgettable night skies.",
                    Price = 7400,
                    Occupancy = 12,
                    ImageUrl = "https://placehold.co/600x405"
                },
                new Hotel
                {
                    Id = 6,
                    Name = "Forest Hideaway",
                    Description = "Secluded cabins surrounded by ancient trees and wildlife.",
                    Price = 3300,
                    Occupancy = 8,
                    ImageUrl = "https://placehold.co/600x406"
                }
                );
        }
    }
}
