using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHVolunteerAPIData.Models.ModelConfigurations
{
    public class IHVolunteerAPIConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(prop => prop.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.VolunteerHours)
                .IsRequired();

            builder.Property(prop => prop.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
