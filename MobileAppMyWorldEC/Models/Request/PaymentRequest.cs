using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileAppMyWorldEC.Models.Response;

namespace MobileAppMyWorldEC.Models.Request
{
    public class PaymentRequest : PaymentViewModel
    {
        public bool IsBonusPayment { get; set; }
    }
}