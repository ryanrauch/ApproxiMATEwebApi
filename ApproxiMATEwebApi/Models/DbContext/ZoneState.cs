using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class ZoneState
    {
        [Key]
        public int StateId { get; set; }
        public String Description { get; set; }         //"Texas"
        public String ShortDescription { get; set; }    //"TX"
    }
}
