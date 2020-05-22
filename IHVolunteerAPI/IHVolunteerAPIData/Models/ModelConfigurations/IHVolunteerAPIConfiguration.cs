using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHVolunteerAPIData.Models.ModelConfigurations
{
    public class IHVolunteerAPIConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Email);

            builder.Property(prop => prop.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(prop => prop.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.VolunteerHours)
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<LoginUser> builder)
        {
            builder.HasKey(prop => prop.Email);

            builder.Property(prop => prop.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.Password)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(prop => prop.Secret);
        }
    }
}
