using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi
{
    public static class CoordinateFilters
    {
        public static double LatitudeBound(double input)
        {
            return Math.Floor(input * 100) / 100;
        }

        public static double LongitudeBound(double input)
        {
            return LatitudeBound(input);
        }

        //Latitude x Longitude
        //bounding box example: "30.40x-97.72" [string] [x is defined in constants]
        //bounding box always contains floor values
        public static double GetLatitudeFloorFromBox(string box)
        {
            if (box.Contains(Constants.BoundingBoxDelim))
                return Double.Parse(box.Split(Constants.BoundingBoxDelim)[0]);
            return 0;
        }
        public static double GetLatitudeCeilingFromBox(string box)
        {
            return GetLatitudeFloorFromBox(box) + 0.01;
        }
        public static double GetLongitudeFloorFromBox(string box)
        {
            if (box.Contains(Constants.BoundingBoxDelim))
                return Double.Parse(box.Split(Constants.BoundingBoxDelim)[1]);
            return 0;
        }
        public static double GetLongitudeCeilingFromBox(string box)
        {
            return GetLongitudeFloorFromBox(box) + 0.01;
        }
    }
}
