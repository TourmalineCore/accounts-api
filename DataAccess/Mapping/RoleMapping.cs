using Core.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping;

internal class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .HasConversion<string>();

        builder.HasData(
                new Role(MappingData.AdminRoleId, BaseRoleNames.Admin, MappingData.AllPermissions),
                new Role(MappingData.CeoRoleId, BaseRoleNames.Ceo, MappingData.AllPermissions)
            );
    }
}