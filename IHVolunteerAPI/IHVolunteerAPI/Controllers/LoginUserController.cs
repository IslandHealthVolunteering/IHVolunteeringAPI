using Microsoft.AspNetCore.Mvc;
using System.Linq;
using IHVolunteerAPIData.Models;
using IHVolunteerAPI.Security;
using Microsoft.EntityFrameworkCore;
using System;

namespace IHVolunteerAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginUserController : Controller
    {
        readonly IHVolunteerAPIContext Context;

        public LoginUserController(IHVolunteerAPIContext context)
            => Context = context;

        [HttpPost]
        public IActionResult LoginUser([FromBody] LoginUser loginUser)
        {
            // get user info from request body
            var user = loginUser;

            if (null == user || null == user.Email || null == user.Password)
            {
                return BadRequest("Please provide all required fields in the body of your request");
            }

            // check to see if user exists in DB
            var userFromDB = Context.LoginUser
                .SingleOrDefault(u => u.Email == user.Email);

            // verify password if user exists in DB
            if (null != userFromDB)
            {
                bool isPasswordValid = PasswordHash.ValidatePassword(user.Password, userFromDB.Password);

                if (isPasswordValid)
                {
                    return Ok("Successfully authenticated");
                }
                else
                {
                    return Unauthorized("Incorrect login credentials");
                }
            }

            // create login user if one does not already exist
            LoginUser loginUserToCreate = new LoginUser()
            {
                Email = user.Email,
                Password = PasswordHash.HashPassword(user.Password)
            };

            try
            {
                Context.Add(loginUserToCreate);
                Context.SaveChanges();
            }
            catch (DbUpdateException dbe)
            {
                Console.WriteLine("An error occurred when creating the entry " + dbe);

                return BadRequest("Please try again later");
            }

            return Created("/api/user", "User " + user.Email + " successfully created");
        }
    }
}
