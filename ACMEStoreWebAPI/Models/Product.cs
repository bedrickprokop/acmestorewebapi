using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEStoreWebAPI.Models
{
    public class Product
    {
        public Int32 id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String pictureUrl { get; set; }
        public Double unitPrice { get; set; }
        public String status { get; set; }
        public Int32 ownerId { get; set; }
    }
}
