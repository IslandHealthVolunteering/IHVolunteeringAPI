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
            var userFromDB = Context.User
                .SingleOrDefault(u => u.Email == email);

            if (null != userFromDB)
            {
                return Ok(userFromDB);
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateUser()
        {
            string original = "Please let me dip them mannn";

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {
                byte[] key = new byte[]
                {
                    145, 244, 24, 44, 98, 192, 234, 23,
                    97, 88, 91, 246, 77, 59, 25, 56
                };
                myAes.Key = key;
          
                // Encrypt the string to an array of bytes.
                byte[] encrypted = AES.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                // Decrypt the bytes to a string.
                string decrypted = AES.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
            }

            return Ok("done");
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
