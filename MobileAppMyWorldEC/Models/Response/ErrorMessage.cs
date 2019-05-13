using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppMyWorldEC.Models.Response
{
    class ErrorMessage
    {
        public bool Success { get; set; }
        public int ErrorNum { get; set; }
        public string ErrorMessages { get; set; }
    }
}
