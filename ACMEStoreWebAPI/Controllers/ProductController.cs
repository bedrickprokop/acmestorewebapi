using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMEStoreWebAPI.Models;

namespace ACMEStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context) {
            _context = context;
        }

        // GET api/product
        [HttpGet]
        public IActionResult List(long ownerId, String status, int page)
        {
            List<Product> productList = new List<Product>();

            User user = new User();
            user.id = 2;
            user.email = "grecks.shake@gmail.com";
            user.money = 300;
            user.type = "ROADRUNNER";
            user.productIdList = new List<long>();

            for (int i = 0; i < 15; i++) {
                Product product = new Product();
                product.id = i+1;
                product.name = "Product " + i + 1;
                product.pictureUrl = "http://www.teste.com";
                product.description = "Product teste" + i + 1;
                product.status = "TOSELL";
                product.unitPrice = 40.33 + i;
                product.ownerId = user.id;
                productList.Add(product);
            }
            return new OkObjectResult(productList);
        }

        // GET api/product/5
        [HttpGet("{productId}")]
        public IActionResult FindById(int productId)
        {
            Product product = new Product();
            product.id = productId;
            product.name = "Product 1";
            product.pictureUrl = "http://www.teste.com";
            product.description = "Product teste one";
            product.status = "TOSELL";
            product.unitPrice = 40.33;

            User user = new User();
            user.id = 2;
            user.email = "grecks.shake@gmail.com";
            user.money = 300;
            user.type = "ROADRUNNER";
            user.productIdList = new List<long>();
            product.ownerId = user.id;

            return new OkObjectResult(product);
        }

        // POST api/product
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (null == product)
            {
                return null;
            }
            else
            {
                product.id = 1;
                return new OkObjectResult(product);
            }
        }

        // DELETE api/product/5
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            Product product = new Product();
            product.id = productId;
            product.name = "Product 1";
            product.pictureUrl = "http://www.teste.com";
            product.description = "Product teste one";
            product.status = "TOSELL";
            product.unitPrice = 40.33;

            User user = new User();
            user.id = 2;
            user.email = "grecks.shake@gmail.com";
            user.money = 300;
            user.type = "ROADRUNNER";
            user.productIdList = new List<long>();
            product.ownerId = user.id;
            return new OkObjectResult(product);
        }

        // POST api/product/buy
        [HttpPost("buy/{productId}")]
        public IActionResult Buy(int productId)
        {
            return new OkObjectResult(true);
        }

        // POST api/product/sell
        [HttpPost("sell/{productId}")]
        public IActionResult Sell(int productId)
        {
            return new OkObjectResult(true);
        }
    }
}
