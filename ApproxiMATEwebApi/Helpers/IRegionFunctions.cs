using System.Collections.Generic;
using ApproxiMATEwebApi.Models;

namespace ApproxiMATEwebApi.Helpers
{
    public interface IRegionFunctions
    {
        T Max<T>(T a, T b);
        T Min<T>(T a, T b);
        bool PointInsidePolygon(List<ZoneRegionPolygon> polygon, double latitude, double longitude);
        bool PointOnPolygonExterior(List<ZoneRegionPolygon> polygon, double latitude, double longitude);
    }
}