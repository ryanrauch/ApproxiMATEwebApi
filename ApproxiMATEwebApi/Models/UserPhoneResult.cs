using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class UserPhoneResult
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
