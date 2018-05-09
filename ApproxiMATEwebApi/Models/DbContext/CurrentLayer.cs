﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class CurrentLayer
    {
        [Key]
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public String LayersDelimited { get; set; }
    }
}
