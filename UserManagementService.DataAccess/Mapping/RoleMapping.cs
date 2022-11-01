using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using UserManagementService.Core.Entities;

namespace UserManagementService.DataAccess.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(p => p.Privileges)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePrivileges",
                    r => r.HasOne<Privilege>().WithMany().HasForeignKey("PrivilegesId"),
                    l => l.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                    je =>
                    {
                        je.HasKey("PrivilegesId", "RolesId");
                        je.HasData(
                            new { PrivilegesId = 1L, RolesId = 1L },
                            new { PrivilegesId = 1L, RolesId = 2L },
                            new { PrivilegesId = 2L, RolesId = 3L });
                    });

            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasData(new Role(1, RoleNames.Admin), 
                            new Role(2, RoleNames.Seo), 
                            new Role(3, RoleNames.Employee));
            
        }
    }
}
