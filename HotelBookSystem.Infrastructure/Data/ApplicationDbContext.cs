using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelNumber> HotelNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Mirage",
                    Description = "Современный отель Mirage предлагает стильный и комфортный отдых в самом сердце города." +
                    " Интерьеры сочетают в себе элегантность и минимализм, создавая атмосферу покоя и уюта." +
                    " Идеально подходит как для деловых поездок, так и для туристов.",
                    Price = 180,
                    Occupancy = 30,
                    ImageUrl = "\\images\\HotelImages\\8ad226ff-832a-4db3-a6b4-b3cc4a6287a6.png"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Veronica",
                    Description = "Hotel Veronica — это уютный 3-звездочный отель для тех, кто ищет комфортное и доступное жильё." +
                    " Он расположен в спокойном районе, недалеко от общественного транспорта и популярных достопримечательностей.",
                    Price = 90,
                    Occupancy = 30,
                    ImageUrl = "\\images\\HotelImages\\880d4c1c-bf94-497e-ab9d-c1258a386b01.jpg"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Marina",
                    Description = "Отель \"Marina\" — это идеальное место для отдыха у моря." +
                    " С его террасы открывается захватывающий вид на бескрайний океан." +
                    " Номера оформлены в современном морском стиле с использованием натуральных материалов и цветов," +
                    " создающих атмосферу уюта и свежести.",
                    Price = 220,
                    Occupancy = 30,
                    ImageUrl = "\\images\\HotelImages\\e19e7191-30d0-49f1-82a9-8442f105df3b.jpeg"
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Palace",
                    Description = "Отель Palace — это уникальное место для тех, кто ценит роскошь и комфорт. " +
                    "Номера, оформленные в классическом стиле, с панорамными окнами, предлагают потрясающий вид на город.",
                    Price = 300,
                    Occupancy = 30,
                    ImageUrl = "\\images\\HotelImages\\66787e93-c4fe-4ab0-a741-8fb47dfa9440.png"
                }
            );

            modelBuilder.Entity<HotelNumber>().HasData(
                new HotelNumber
                {
                    Hotel_Number = 101,
                    HotelId = 1,
                },
                new HotelNumber
                {
                    Hotel_Number = 102,
                    HotelId = 1,
                },
                new HotelNumber
                {
                    Hotel_Number = 103,
                    HotelId = 1,
                },
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
                    Hotel_Number = 303,
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
                    HotelId = 1,
                    Name = "Private Pool"
                },
                new Amenity
                {
                    Id = 2,
                    HotelId = 1,
                    Name = "Microwave"
                },
                new Amenity
                {
                    Id = 3,
                    HotelId = 1,
                    Name = "Private Balcony"
                },
                new Amenity
                {
                    Id = 4,
                    HotelId = 1,
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
                },
                new Amenity
                {
                    Id = 12,
                    HotelId = 4,
                    Name = "Infinity Pool"
                },
                new Amenity
                {
                    Id = 13,
                    HotelId = 4,
                    Name = "Spa and Wellness Center"
                },
                new Amenity
                {
                    Id = 14,
                    HotelId = 4,
                    Name = "Private Beach Access"
                }
            );
        }
    }
}
