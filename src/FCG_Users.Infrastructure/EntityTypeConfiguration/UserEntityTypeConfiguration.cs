using FCG_Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG_Users.Infrastructure.EntityTypeConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.UserName).IsRequired().HasMaxLength(50);
        builder.Property(user => user.Email).IsRequired().HasMaxLength(50);
        builder.Property(user => user.Password).IsRequired();
        builder.Property(user => user.Role).HasConversion<string>().IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();
    }
}