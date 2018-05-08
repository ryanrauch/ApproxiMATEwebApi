using System.Collections.Generic;

namespace ApproxiMATEwebApi.Models
{
    public class Polygon
    {
        public IList<Position> Positions { get; set; }
        public IList<Position> Holes { get; set; }
        public object Tag { get; set; }
    }
}
