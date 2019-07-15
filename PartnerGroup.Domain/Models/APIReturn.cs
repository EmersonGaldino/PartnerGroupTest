using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerGroup.Domain.Models
{
    public class APIReturn
    {
        public int StatusCode { get; set; }
        public dynamic Content { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}
