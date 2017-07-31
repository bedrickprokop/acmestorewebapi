using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACMEStoreWebAPI.Models
{
    public class User
    {
        public long id { get; set; }
        public String email { get; set; }
        public String type { get; set; }
        public Double money { get; set; }
        public List<Product> productList { get; set; }
    }
}
