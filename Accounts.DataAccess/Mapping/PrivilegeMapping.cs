using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.Mapping
{
    public class PrivilegeMapping : IEntityTypeConfiguration<Privilege>
    {
        public void Configure(EntityTypeBuilder<Privilege> builder)
        {
            builder.Property(x => x.Name)
                .HasConversion<string>();

            builder.HasData(new Privilege(1, PrivilegeNames.CanManageEmployees),
                            new Privilege(2, PrivilegeNames.CanViewAnalytic),
                            new Privilege(3, PrivilegeNames.CanViewFinanceForPayroll));
        }
    }
}
