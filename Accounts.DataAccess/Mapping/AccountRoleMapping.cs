using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.Mapping
{
    internal class AccountRoleMapping : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.RoleId });

            builder.HasOne(x => x.Account)
                .WithMany(x => x.AccountRoles)
                .HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.Role)
             .WithMany(x => x.AccountRoles)
             .HasForeignKey(x => x.RoleId);
        }
    }
}
