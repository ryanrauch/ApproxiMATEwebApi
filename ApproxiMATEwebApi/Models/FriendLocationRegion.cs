using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class FriendLocationRegion
    {
        public int RegionID { get; set; }
        public List<FriendLocationResult> Friends { get; set; }
    }
}
