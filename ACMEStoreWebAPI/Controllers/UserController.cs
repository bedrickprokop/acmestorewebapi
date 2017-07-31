using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACMEStoreWebAPI.Models;

namespace ACMEStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (null == user) {
                return null;
            }
            else
            {
                user.id = 1;
                return new OkObjectResult(user);
            }
        }

        // DELETE api/user/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            User user = new User();
            user.id = userId;
            user.email = "grecks.shake@gmail.com";
            user.money = 300;
            user.type = "ROADRUNNER";
            user.productList = new List<Product>();

            return new OkObjectResult(user);
        }
    }
}
