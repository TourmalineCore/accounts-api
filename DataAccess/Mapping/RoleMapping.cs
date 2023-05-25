using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping;

internal class RoleMapping : IEntityTypeConfiguration<Role>
{
    private readonly List<Permission> _allPermissions = new()
    {
        new Permission(Permissions.ViewPersonalProfile),
        new Permission(Permissions.EditPersonalProfile),
        new Permission(Permissions.ViewContacts),
        new Permission(Permissions.ViewSalaryAndDocumentsData),
        new Permission(Permissions.EditFullEmployeesData),
        new Permission(Permissions.AccessAnalyticalForecastsPage),
        new Permission(Permissions.ViewAccounts),
        new Permission(Permissions.ManageAccounts),
        new Permission(Permissions.ViewRoles),
        new Permission(Permissions.ManageRoles),
    };

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .HasConversion<string>();

        builder.HasData(
                new Role(1, RoleNames.Admin, _allPermissions),
                new Role(2, RoleNames.Ceo, _allPermissions)
            );
    }

    private static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Ceo = "CEO";
    }
}