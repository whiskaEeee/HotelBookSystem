﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelBookSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string Name { get; set; }
        public DateOnly CreationDate { get; set; }

    }
}
