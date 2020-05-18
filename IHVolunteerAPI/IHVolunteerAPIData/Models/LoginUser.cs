using System;
using System.ComponentModel.DataAnnotations;

namespace IHVolunteerAPIData.Models
{
    public class LoginUser
    {
        public LoginUser()
        {
        }

        [Key]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
