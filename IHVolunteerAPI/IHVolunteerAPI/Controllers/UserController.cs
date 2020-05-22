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
        {
            Context = context;
        }

        [HttpGet("{email}")]
        public IActionResult GetUser([FromRoute] string email)
        {
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
        [Route("create")]
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

        [HttpPost]
        [Route("login")]
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
                    // check to see if login user has a secret associated to them
                    // if not, create one and assign it to them
                    if (null != userFromDB.Secret)
                    {
                        return Ok(userFromDB.Secret);
                    }

                    // Ecryption
                    using (Aes myAes = Aes.Create())
                    {
                        var secret = AES.RandomString(24);

                        // Encrypt the string to an array of bytes.
                        byte[] encrypted = AES.EncryptStringToBytes_Aes(secret);
                        string encryptedString = BitConverter.ToString(encrypted);

                        try
                        {
                            userFromDB.Secret = encryptedString;
                            Context.SaveChanges();
                        }
                        catch (DbUpdateException dbe)
                        {
                            Console.WriteLine("An error occurred when updating the entries " + dbe);

                            return BadRequest("Please try again later");
                        }

                        return Ok(encryptedString);
                    }
                }
            }

            return Unauthorized("Incorrect login credentials");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser([FromBody] LoginUser loginUser)
        {
            var user = loginUser;

            if (null == user || null == user.Email || null == user.Password)
            {
                return BadRequest("Please make sure you provide all the required parameters");
            }

            // check to see if user exists in DB
            var userFromDB = Context.LoginUser
                .SingleOrDefault(u => u.Email == user.Email);

            if (null != userFromDB && null != userFromDB.Email)
            {
                return BadRequest("Please try again later");
            }

            // Ecryption
            using (Aes myAes = Aes.Create())
            {
                var secret = AES.RandomString(24);

                // Encrypt the string to an array of bytes.
                byte[] encrypted = AES.EncryptStringToBytes_Aes(secret);

                // create login user
                LoginUser loginUserToCreate = new LoginUser()
                {
                    Email = user.Email,
                    Password = PasswordHash.HashPassword(user.Password),
                    Secret = BitConverter.ToString(encrypted)
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

                return Created("/api/registeruser", BitConverter.ToString(encrypted));
            }
        }
    }
}
