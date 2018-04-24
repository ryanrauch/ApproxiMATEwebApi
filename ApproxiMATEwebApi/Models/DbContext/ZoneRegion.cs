using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class ZoneRegion
    {
        [Key]
        public int RegionId { get; set; }
        public String Description { get; set; }
        public int Type { get; set; }               //enum
        public ZoneCity City { get; set; }
        public double BoundLatitudeMin { get; set; }
        public double BoundLatitudeMax { get; set; }
        public double BoundLongitudeMin { get; set; }
        public double BoundLongitudeMax { get; set; }
        public String ARGBFill { get; set; }
        public String ARGBStroke { get; set; }
        public float StrokeWidth { get; set; }
    }
}
