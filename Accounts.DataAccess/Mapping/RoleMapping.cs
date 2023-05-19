using System.Collections.Generic;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.Mapping;

internal class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .HasConversion<string>();

        builder.HasData(new Role(1,
                        RoleNames.Admin,
                        new List<Permission>
                        {
                            new(Permissions.CanManageEmployees),
                        }
                    ),
                new Role(2,
                        RoleNames.CEO,
                        new List<Permission>
                        {
                            new(Permissions.CanManageEmployees),
                            new(Permissions.CanViewAnalytic),
                            new(Permissions.CanViewFinanceForPayroll),
                        }
                    ),
                new Role(3,
                        RoleNames.Manager,
                        new List<Permission>
                        {
                            new(Permissions.CanManageEmployees),
                        }
                    )
            );
    }
}