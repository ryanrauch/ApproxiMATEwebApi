﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
