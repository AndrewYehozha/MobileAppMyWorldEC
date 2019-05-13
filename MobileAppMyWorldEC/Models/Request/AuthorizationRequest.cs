using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileAppMyWorldEC.Models.Request
{
    public class AuthorizationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}