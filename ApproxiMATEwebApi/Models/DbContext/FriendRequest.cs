using ApproxiMATEwebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class FriendRequest
    {
        public String InitiatorId { get; set; }
        public ApplicationUser Initiator { get; set; }
        public String TargetId { get; set; }
        public ApplicationUser Target { get; set; }
        public DateTime TimeStamp { get; set; }
        public FriendRequestType? Type { get; set; }
        public Boolean TargetViewed { get; set; }
    }
}
