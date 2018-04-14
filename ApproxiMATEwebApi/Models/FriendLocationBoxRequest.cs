using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class FriendLocationBoxRequest
    {
        public Guid UserId { get; set; }
        public string BoundingBox { get; set; }
    }
}
