using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IHVolunteerAPIData.Models
{
    public class User
    {
        public User()
        {
        }

        public string Name { get; set; }

        public int VolunteerHours { get; set; }

        [Key]
        [ForeignKey("Email")]
        public string Email { get; set; }
    }
}
