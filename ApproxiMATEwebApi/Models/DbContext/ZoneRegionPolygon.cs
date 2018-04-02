using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class ZoneRegionPolygon
    {
        [Key]
        [Column(Order=1)]
        public ZoneRegion Region { get; set; }
        [Key]
        [Column(Order=2)]
        public int Order { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    /*
     * Note that each Polygon defined will be a sequential list of integers, ie: 1,2,3,4,5,6
     * if more than one Polygon is defined, there will be a gap in the Order, ie: 1,2,3,4,5,6,10,11,12,13 (defines 1:6, and 10:13)
     * Interior Polygons (used to remove area) are defined with negative Order values, 
     * and interior Polygons are assigned to matching parent Polygon's by lowest value of each.
     * ie: -15,-14,-13,-12,-11,-10,1,2,3,4,5,6,10,11,12,13 will assign the interior polygon of (-15:-10 to the polygon 10:13)
     * essentially creating two area's, 1:6 without any holes, and 10:13 minus the area within -15:-10
     */ 
}
