using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class CurrentLayer
    {
        [Key]
        public Guid UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Layer1Latitude { get; set; }
        public double Layer1Longitude { get; set; }
        public double Layer3Latitude { get; set; }
        public double Layer3Longitude { get; set; }
        public double Layer9Latitude { get; set; }
        public double Layer9Longitude { get; set; }
        public double Layer27Latitude { get; set; }
        public double Layer27Longitude { get; set; }
        public double Layer81Latitude { get; set; }
        public double Layer81Longitude { get; set; }
        public double Layer243Latitude { get; set; }
        public double Layer243Longitude { get; set; }
        public double Layer729Latitude { get; set; }
        public double Layer729Longitude { get; set; }
        public double Layer2187Latitude { get; set; }
        public double Layer2187Longitude { get; set; }
    }
}
