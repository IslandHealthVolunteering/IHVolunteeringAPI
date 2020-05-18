using System;
using System.ComponentModel.DataAnnotations;

namespace IHVolunteerAPIData.Models
{
    public class User
    {
        public User()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int VolunteerHours { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
