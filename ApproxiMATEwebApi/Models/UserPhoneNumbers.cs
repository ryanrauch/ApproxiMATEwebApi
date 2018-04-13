using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class UserPhoneNumbers
    {
        public Guid UserId { get; set; }
        public List<string> Numbers { get; set; }
    }
}
