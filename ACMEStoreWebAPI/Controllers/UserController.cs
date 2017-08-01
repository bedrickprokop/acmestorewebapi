using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ACMEStoreWebAPI.Models;

namespace ACMEStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserContext _userContext;
        private readonly ProductContext _productContext;

        public UserController(UserContext userContext, ProductContext productContext)
        {
            _userContext = userContext;
            _productContext = productContext;

            if (_userContext.UserItems.Count() == 0)
            {
                _userContext.UserItems.Add(new User { id = 1, email = "coyote@gmail.com", money = 3000, token = "" });
                _userContext.UserItems.Add(new User { id = 2, email = "roadrunner@hotmail.com", money = 3000, token = "" });
                _userContext.SaveChanges();
            }
            if (_productContext.ProductItems.Count() == 0) {

                _productContext.ProductItems.Add(new Product
                {
                    id = 1,
                    name = "EarthQuake Pills",
                    description = "ACME EarthQuake Pills - Why Wait? Make your own earthquakes - loads of fun",
                    pictureUrl = "http://static.tvtropes.org/pmwiki/pub/images/acme.jpg",
                    unitPrice = 100,
                    status ="TOSELL",
                    ownerId = 2});

                _productContext.ProductItems.Add(new Product
                {
                    id = 2,
                    name = "Super Speed Vitamins",
                    description = "ACME Super Speed Vitamins",
                    pictureUrl = "http://acme.com/catalog/supers.jpg",
                    unitPrice = 130,
                    status = "TOSELL",
                    ownerId = 1
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 3,
                    name = "Giant Rubber Band",
                    description = "ACME Brand - One Giant Rubber Band(For tripping road-runners)",
                    pictureUrl = "http://acme.com/catalog/giantrubber2.jpg",
                    unitPrice = 80,
                    status = "TOSELL",
                    ownerId = 1
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 4,
                    name = "Giant Rubber Band",
                    description = "One ACME Giant Rubber Band - Fantastic Elastic",
                    pictureUrl = "http://acme.com/catalog/giantrubber3.jpg",
                    unitPrice = 85,
                    status = "TOSELL",
                    ownerId = 1
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 5,
                    name = "Explosive Tennis Balls",
                    description = "Tickle your friends! Surprise your opponent!",
                    pictureUrl = "http://acmeltd.org/home/wp-content/uploads/wpsc/product_images/explosivetennisballs.jpg",
                    unitPrice = 160,
                    status = "TOSELL",
                    ownerId = 2
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 6,
                    name = "Giant Kite Kit",
                    description = "ACME Giant Kite Kit",
                    pictureUrl = "http://vignette1.wikia.nocookie.net/tinytoons/images/6/64/Acme.jpg/revision/latest?cb=20130211143138",
                    unitPrice = 420,
                    status = "TOSELL",
                    ownerId = 2
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 7,
                    name = "Fleet Foot",
                    description = "ACME Jet-Propelled Tennis Shoes",
                    pictureUrl = "http://s-media-cache-ak0.pinimg.com/originals/63/60/58/6360586fcb0275ff0e24e4e490318ef3.jpg",
                    unitPrice = 500,
                    status = "TOSELL",
                    ownerId = 1
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 8,
                    name = "Desintegrating Pistol",
                    description = "ACME Desintegrating Pistol",
                    pictureUrl = "http://img.ibxk.com.br/2013/7/materias/814635873415612.jpg",
                    unitPrice = 800,
                    status = "TOSELL",
                    ownerId = 2
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 9,
                    name = "Axle Grease",
                    description = "ACME Axle Grease - Guaranteed slippery",
                    pictureUrl = "http://s-media-cache-ak0.pinimg.com/originals/da/f5/a5/daf5a51c5a6a4a8aed0c8d39faf779f7.jpg",
                    unitPrice = 50,
                    status = "TOSELL",
                    ownerId = 2
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 10,
                    name = "Jet Propelled Pogo Stick",
                    description = "One ACME Jet Propelled Pogo Stick",
                    pictureUrl = "http://www.nerdcore.de/ACME/jetprop.jpg",
                    unitPrice = 185,
                    status = "TOSELL",
                    ownerId = 1
                });

                _productContext.ProductItems.Add(new Product
                {
                    id = 11,
                    name = "Hair Grower",
                    description = "ACME Hair Grower - Guaranteed",
                    pictureUrl = "http://shanegela.github.io/FEWD/homework/homework-3/images/hair.jpg",
                    unitPrice = 377,
                    status = "TOSELL",
                    ownerId = 2
                });
                _productContext.SaveChanges();
            }

        }

        // GET api/user
        [HttpGet]
        public IActionResult List()
        {
            List<User> userList = _userContext.UserItems.ToList();
            return new OkObjectResult(userList);
        }

        // GET api/user/5
        [HttpGet("{userId}")]
        public IActionResult FindById(Int32? userId)
        {
            if (null != userId)
            {
                User user = _userContext.UserItems.First(t => t.id == userId);
                return new OkObjectResult(user);
            }
            else
            {
                return new OkObjectResult(new User());
            }
        }


        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (null != user) {
                int total = _userContext.UserItems.Count();
                Int32 lastId = 1;

                if (total > 0) {
                    User lastUser = _userContext.UserItems.Last();
                    if (null != lastUser)
                    {
                        lastId = lastUser.id + 1;
                    }
                }

                user.id = lastId;
                _userContext.UserItems.Add(user);
                _userContext.SaveChanges();

                return new OkObjectResult(user);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/user/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(Int32? userId)
        {
            if (null != userId) {
                User user = _userContext.UserItems.FirstOrDefault(t => t.id == userId);
                if (null != user)
                {
                    _userContext.UserItems.Remove(user);
                    _userContext.SaveChanges();
                    
                    List<Product> productsToRemove = _productContext.ProductItems
                        .Where(t => t.ownerId.Equals(user.id)).ToList();

                    if (null != productsToRemove && productsToRemove.Count() > 0)
                    {
                        _productContext.ProductItems.RemoveRange(productsToRemove);
                        _productContext.SaveChanges();
                    }
                    return new OkObjectResult(user);
                }
                else
                {
                    return NotFound();
                }
            } else
            {
                return BadRequest();
            }
        }
       
    }
}
