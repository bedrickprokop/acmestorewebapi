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
        }

        // GET api/user
        [HttpGet]
        public IActionResult List()
        {
            List<User> userList = _userContext.UserItems.ToList();
            return new OkObjectResult(userList);
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
