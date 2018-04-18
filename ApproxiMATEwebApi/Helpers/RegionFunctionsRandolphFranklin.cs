using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Models;

namespace ApproxiMATEwebApi.Helpers
{
    public class RegionFunctionsRandolphFranklin : RegionFunctions
    {
        public override bool PointInsidePolygon(List<ZoneRegionPolygon> polygon, double latitude, double longitude)
        {
            //TODO: MUST DISTINGUISH EACH POLYGON, AND/OR HOLES CONTAINED WITHIN
            //
            //http:--//paulbourke.net/geometry/polygonmesh/#insidepoly
            //https:--//wrf.ecse.rpi.edu//Research/Short_Notes/pnpoly.html
            int npol = polygon.Count;
            bool c = false;
            for (int i = 0, j = npol - 1; i < npol; j = i++)
            {
                if ((((polygon[i].Longitude <= longitude) && (longitude < polygon[j].Longitude))
                    || ((polygon[j].Longitude <= longitude) && (longitude < polygon[i].Longitude)))
                    && (latitude < (polygon[j].Latitude - polygon[i].Latitude) * (longitude - polygon[i].Longitude) / (polygon[j].Longitude - polygon[i].Longitude) + polygon[i].Latitude))
                {
                    c = !c;
                }
            }
            return c;
        }
    }
}
