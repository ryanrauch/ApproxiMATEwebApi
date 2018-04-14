using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class FriendLocationBox
    {
        public string BoundingBox { get; set; }
        public List<FriendLocationResult> Friends { get; set; }
    }
}
