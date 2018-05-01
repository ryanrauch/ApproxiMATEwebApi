using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models.DataContracts
{
    public class CurrentLocationPost
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double ZoomLevel { get; set; }
    }
}
