using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.Mapping
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasData(new Permission(1, PermissionNames.CanManageEmployees),
                            new Permission(2, PermissionNames.CanViewAnalytic),
                            new Permission(3, PermissionNames.CanViewFinanceForPayroll));

            builder.ToTable("Permissions");
        }
    }
}
