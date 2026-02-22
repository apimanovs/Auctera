using Auctera.Identity.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auctera.Persistance.Configurations.Users;

public sealed class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(user => user.PasswordHash)
            .IsRequired();

        builder.Property(user => user.IsAdmin)
            .IsRequired();

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.HasIndex(user => user.UserName)
            .IsUnique();
    }
}
