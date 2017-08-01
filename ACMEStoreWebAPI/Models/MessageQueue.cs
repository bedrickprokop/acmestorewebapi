using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEStoreWebAPI.Models
{
    public class MessageQueue
    {
        public Int32 productId { get; set; }
        public Boolean fromNotification { get; set; }
        public String fromView { get; set; }
        public User user { get; set; }
        public String message { get; set; }
    }
}
