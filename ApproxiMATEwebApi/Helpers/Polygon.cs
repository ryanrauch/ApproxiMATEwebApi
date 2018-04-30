using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public class Polygon
    {
        public IList<Position> Positions { get; set; }
        public IList<Position> Holes { get; set; }
        public object Tag { get; set; }
    }
}
