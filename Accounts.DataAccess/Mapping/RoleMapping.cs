using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Accounts.DataAccess.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(p => p.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesPermissions",
                    r => r.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                    l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    je =>
                    {
                        je.HasKey("PermissionId", "RoleId");
                        je.HasData(
                            new { PermissionId = 1L, RoleId = 1L },
                            new { PermissionId = 1L, RoleId = 2L },
                            new { PermissionId = 2L, RoleId = 2L },
                            new { PermissionId = 3L, RoleId = 2L },
                            new { PermissionId = 1L, RoleId = 3L });
                    });

            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasData(new Role(1, RoleNames.Admin),
                            new Role(2, RoleNames.CEO),
                            new Role(3, RoleNames.Manager),
                            new Role(4, RoleNames.Employee));
        }
    }
}
