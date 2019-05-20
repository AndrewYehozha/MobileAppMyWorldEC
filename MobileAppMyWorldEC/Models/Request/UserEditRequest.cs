﻿using System;

namespace MobileAppMyWorldEC.Models.Request
{
    public class UserEditRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Birsday { get; set; }
    }
}