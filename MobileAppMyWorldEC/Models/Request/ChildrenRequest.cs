using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileAppMyWorldEC.Models.Response;

namespace MobileAppMyWorldEC.Models.Request
{
    public class ChildrenRequest : ChildrenViewModel
    {
        public int UserId { get; set; }
    }
}