using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileAppMyWorldEC.Models.Response
{
    public class UserViewModel
    {
        public bool Success { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public DateTime? Birsday { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdministration { get; set; }
        public int BonusScore { get; set; }
    }
}