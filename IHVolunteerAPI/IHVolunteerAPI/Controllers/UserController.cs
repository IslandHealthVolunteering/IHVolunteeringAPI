using System.Linq;
using IHVolunteerAPIData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using IHVolunteerAPI.Security;
using System.Text;

namespace IHVolunteerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly IHVolunteerAPIContext Context;

        public UserController(IHVolunteerAPIContext context)
            => Context = context;

        [HttpGet("{email}")]
        public IActionResult GetUser([FromRoute] string email)
        {
            // try pulling env var
            var variable = Environment.GetEnvironmentVariable("SECRET_KEY");
            Console.WriteLine("environment variable is: " + variable);

            var userFromDB = Context.User
                .SingleOrDefault(u => u.Email == email);

            if (null != userFromDB)
            {
                return Ok(userFromDB);
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var userToUpdate = user;

            try
            {
                Context.Update(userToUpdate);
                Context.SaveChanges();
            }
            catch(DbUpdateException dbe)
            {
                Console.WriteLine("An error occurred when updating the entries " + dbe);

                return BadRequest("Please try again later");
            }

            return Ok("User " + userToUpdate.Email + " updated successfully");
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var userToCreate = user;

            try
            {
                Context.Add(userToCreate);
                Context.SaveChanges();
            }
            catch(DbUpdateException dbe)
            {
                Console.WriteLine("An error occurred when updating the entries " + dbe);

                return BadRequest("Please make sure you have created login creds with this email address");
            }

            return Created("/api/user", "User " + userToCreate.Email + " successfully created");
        }
    }
}
