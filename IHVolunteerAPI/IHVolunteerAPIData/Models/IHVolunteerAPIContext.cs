using System;
using IHVolunteerAPIData.Models.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IHVolunteerAPIData.Models
{
    public class IHVolunteerAPIContext : DbContext
    {
        public IHVolunteerAPIContext(DbContextOptions<IHVolunteerAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfiguration(new IHVolunteerAPIConfiguration());

        public DbSet<User> User { get; set; }
    }
}
