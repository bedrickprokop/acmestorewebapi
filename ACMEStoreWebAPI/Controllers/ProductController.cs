using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ACMEStoreWebAPI.Models;

namespace ACMEStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductContext _productContext;

        public ProductController(ProductContext productContext) {
            _productContext = productContext;
        }

        // GET api/product
        [HttpGet]
        public IActionResult List()
        {
            List<Product> productList = _productContext.ProductItems.ToList();
            return new OkObjectResult(productList);
        }

        // GET api/product/query?ownerid=1&status=TOSELL&page=1
        [HttpGet("query")]
        public IActionResult List([FromQuery] Int32? ownerId, [FromQuery] String status, [FromQuery] Int32? page)
        {
            if (ownerId == null && status != null)
            {
                List<Product> productList = _productContext.ProductItems
                .Where(product => product.status.Equals(status)).ToList();

                return new OkObjectResult(productList);
            }
            else if (ownerId != null && status != null)
            {
                List<Product> productList = _productContext.ProductItems
                .Where(product => product.ownerId == ownerId && product.status.Equals(status)).ToList();

                return new OkObjectResult(productList);
            }
            else
            {
                return new OkObjectResult(new List<Product>(0));
            }
        }

        // GET api/product/5
        [HttpGet("{productId}")]
        public IActionResult FindById(Int32? productId)
        {
            if(null != productId)
            {
                Product product = _productContext.ProductItems.First(t => t.id == productId);
                return new OkObjectResult(product);
            }
            else
            {
                return new OkObjectResult(new Product());
            }
        }

        // POST api/product
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (null != product)
            {
                int total = _productContext.ProductItems.Count();
                product.id = total + 1;
                _productContext.ProductItems.Add(product);
                _productContext.SaveChanges();
                
                return new OkObjectResult(product);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/product/5
        [HttpDelete("{productId}")]
        public IActionResult Delete(Int32? productId)
        {
            if (null != productId)
            {
                Product product = _productContext.ProductItems.FirstOrDefault(t => t.id == productId);
                if (null != product)
                {
                    _productContext.ProductItems.Remove(product);
                    _productContext.SaveChanges();
                    return new OkObjectResult(product);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/product/buy/{buyerId}/{productId}
        [HttpPost("buy/{buyerId}/{productId}")]
        public IActionResult Buy(Int32? buyerId, Int32? productId)
        {
            if (null != buyerId && null != productId)
            {

                Product product = _productContext.ProductItems.First(t => t.id.Equals(productId));
                product.status = "BOUGHT";
                product.ownerId = buyerId.Value;
                _productContext.SaveChanges();
                
                return new OkObjectResult(true);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/product/sell
        [HttpPost("sell/{productId}")]
        public IActionResult Sell(Int32? productId)
        {
            if (null != productId)
            {
                Product product = _productContext.ProductItems.First(t => t.id.Equals(productId));
                product.status = "TOSELL";
                _productContext.SaveChanges();

                return new OkObjectResult(true);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
