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
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (null != user) {
                int total = _context.UserItems.Count();
                long lastId = 1;

                if (total > 0) {
                    User lastUser = _context.UserItems.Last();
                    if (null != lastUser)
                    {
                        lastId = lastUser.id + 1;
                    }
                }

                user.id = lastId;
                _context.UserItems.Add(user);
                _context.SaveChanges();

                return new OkObjectResult(user);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/user/5
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            User user = _context.UserItems.FirstOrDefault(t => t.id == userId);
            if (null != user) {
                _context.UserItems.Remove(user);
                _context.SaveChanges();
                return new OkObjectResult(user);
            } else
            {
                return NotFound();
            }
        }
    }
}
