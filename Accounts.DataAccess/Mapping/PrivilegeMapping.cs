using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementService.Core.Entities;

namespace UserManagementService.DataAccess.Mapping
{
    public class PrivilegeMapping : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasData(new Privilege(1, PrivilegesNames.CanManageEmployees),
                            new Privilege(2, PrivilegesNames.CanViewAnalytic),
                            new Privilege(3, PrivilegesNames.CanViewFinanceForPayroll));
        }
    }
}
