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
        public DbSet<HotelNumber> HotelNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

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

            modelBuilder.Entity<HotelNumber>().HasData(
                new HotelNumber
                {
                    Hotel_Number = 201,
                    HotelId = 2,
                },
                new HotelNumber
                {
                    Hotel_Number = 202,
                    HotelId = 2,
                },
                new HotelNumber
                {
                    Hotel_Number = 203,
                    HotelId = 2,
                },
                new HotelNumber
                {
                    Hotel_Number = 301,
                    HotelId = 3,
                },
                new HotelNumber
                {
                    Hotel_Number = 302,
                    HotelId = 3,
                },
                new HotelNumber
                {
                    Hotel_Number = 401,
                    HotelId = 4,
                },
                new HotelNumber
                {
                    Hotel_Number = 402,
                    HotelId = 4,
                },
                new HotelNumber
                {
                    Hotel_Number = 403,
                    HotelId = 4,
                }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    HotelId = 4,
                    Name = "Private Pool"
                },
                new Amenity
                {
                    Id = 2,
                    HotelId = 4,
                    Name = "Microwave"
                },
                new Amenity
                {
                    Id = 3,
                    HotelId = 4,
                    Name = "Private Balcony"
                },
                new Amenity
                {
                    Id = 4,
                    HotelId = 4,
                    Name = "1 king bed and 1 sofa bed"
                },
                new Amenity
                {
                    Id = 5,
                    HotelId = 2,
                    Name = "Private Plunge Pool"
                },
                new Amenity
                {
                    Id = 6,
                    HotelId = 2,
                    Name = "Microwave and Mini Refrigerator"
                },
                new Amenity
                {
                    Id = 7,
                    HotelId = 2,
                    Name = "Private Balcony"
                },
                new Amenity
                {
                    Id = 8,
                    HotelId = 2,
                    Name = "king bed or 2 double beds"
                },
                new Amenity
                {
                    Id = 9,
                    HotelId = 3,
                    Name = "Private Pool"
                },
                new Amenity
                {
                    Id = 10,
                    HotelId = 3,
                    Name = "Jacuzzi"
                },
                new Amenity
                {
                    Id = 11,
                    HotelId = 3,
                    Name = "Private Balcony"
                }
            );

        }
    }
}
