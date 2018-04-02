using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class ZoneCity
    {
        [Key]
        public int CityId { get; set; }
        public String Description { get; set; }
        public ZoneState State { get; set; }
    }
}
