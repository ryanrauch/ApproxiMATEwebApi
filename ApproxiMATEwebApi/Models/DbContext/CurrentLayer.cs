using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class CurrentLayer
    {
        [Key]
        public Guid UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public String LayersDelimited { get; set; }
    }
}
