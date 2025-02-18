﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HOSTEE.Models;

namespace HOSTEE.Models

{
    public class ApplicationUser : IdentityUser
    {   
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
