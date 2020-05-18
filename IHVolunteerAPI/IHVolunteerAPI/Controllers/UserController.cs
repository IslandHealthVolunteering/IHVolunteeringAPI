using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IHVolunteerAPIData.Models;
using Microsoft.AspNetCore.Mvc;

namespace IHVolunteerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly IHVolunteerAPIContext Context;

        public UserController(IHVolunteerAPIContext context)
            => Context = context;

        [HttpGet]
        public IActionResult GetUser()
        {
            var users = Context.User.ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser()
        {
            var user = new User()
            {
                Name = "Test User",
                Email = "testuser@example.com",
                VolunteerHours = 10,
                Password = "verysecurepassword"
            };

            Context.Add(user);
            Context.SaveChanges();

            return Ok("Successfully created user with email " + user.Email);
        }
    }
}
