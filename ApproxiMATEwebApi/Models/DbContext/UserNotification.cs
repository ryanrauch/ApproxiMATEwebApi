using ApproxiMATEwebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models.DbContext
{
    public class UserNotification
    {
        [Key]
        public Guid NotificationId { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public NotificationType NotificationType { get; set; }
        public Guid? TargetId { get; set; }
    }
}
