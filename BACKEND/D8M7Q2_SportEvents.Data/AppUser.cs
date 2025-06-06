﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D8M7Q2_SportEvents.Data
{
    public class AppUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public AppUser(string username) : base(username)
        {

        }

        public AppUser()
        {

        }
    }
}
