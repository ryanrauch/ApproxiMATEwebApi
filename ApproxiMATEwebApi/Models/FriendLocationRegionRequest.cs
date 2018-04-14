using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class FriendLocationRegionRequest
    {
        public Guid UserId { get; set; }
        public int RegionId { get; set; }
    }
}
