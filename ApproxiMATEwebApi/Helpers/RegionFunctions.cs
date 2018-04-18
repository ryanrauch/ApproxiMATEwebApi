using ApproxiMATEwebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public class RegionFunctions : IRegionFunctions
    {
        public T Min<T>(T a, T b)
        {
            return Comparer<T>.Default.Compare(a, b) < 0 ? a : b;
        }
        public T Max<T>(T a, T b)
        {
            return Comparer<T>.Default.Compare(a, b) > 0 ? a : b;
        }
        public bool PointOnPolygonExterior(List<ZoneRegionPolygon> polygon, double latitude, double longitude)
        {
            return polygon.Exists(p => p.Latitude.Equals(latitude)
                                       && p.Longitude.Equals(longitude));
        }
        public virtual bool PointInsidePolygon(List<ZoneRegionPolygon> polygon, double latitude, double longitude)
        {
            throw new NotImplementedException("Must check this algorithm first, and apply logic to account for holes");
            //http:--//paulbourke.net/geometry/polygonmesh/#insidepoly
            //this implementation treats latitude=>x and longitude=>y
            int counter = 0;
            double xIntersection;
            double p1x = polygon[0].Latitude,
                   p1y = polygon[1].Longitude,
                   p2x,
                   p2y;
            for (int i = 0; i < polygon.Count; ++i)
            {
                p2x = polygon[i].Latitude;
                p2y = polygon[i].Longitude;
                if (longitude > Min(p1y, p2y))
                {
                    if (longitude <= Max(p1x, p2y))
                    {
                        if (latitude <= Max(p1x, p2x))
                        {
                            if (p1y != p2y)
                            {
                                xIntersection = (longitude - p1y) * (p2x - p1x) / (p2y - p1y) + p1x;
                                if (p1x == p2x || latitude <= xIntersection)
                                {
                                    ++counter;
                                }
                            }
                        }
                    }
                }
                p1x = p2x;
                p1y = p2y;
            }
            if (counter % 2 == 0)
                return false;
            return true;
        }
    }
}
