using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileAppMyWorldEC.Models.Response;

namespace MobileAppMyWorldEC.Models.Request
{
    public class Discount_CardRequest : Discount_CardViewModel
    {
        public int UserId { get; set; }
    }
}