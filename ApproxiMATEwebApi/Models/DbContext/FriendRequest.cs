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
        [Key]
        [Column(Order=1)]
        public Guid InitiatorId { get; set; }
        [Key]
        [Column(Order=2)]
        public Guid TargetId { get; set; }
        public DateTime TimeStamp { get; set; }
        public FriendRequestType? Type { get; set; }
    }
}
