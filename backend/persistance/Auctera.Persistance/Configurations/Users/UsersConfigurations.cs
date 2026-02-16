using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Identity.Domain;
using Auctera.Items.Domain;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Persistance.Configurations.Users;
public sealed class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
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
    }
}
