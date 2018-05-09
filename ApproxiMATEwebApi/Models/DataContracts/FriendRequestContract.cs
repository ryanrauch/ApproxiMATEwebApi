using ApproxiMATEwebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models.DataContracts
{
    public class FriendRequestContract
    {
        public String InitiatorId { get; set; }
        public String TargetId { get; set; }
        public DateTime TimeStamp { get; set; }
        public FriendRequestType? Type { get; set; }
    }
}
