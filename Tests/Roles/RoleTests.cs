using Core.Entities;
using Core.Exceptions;
using Core.Models;
using Moq;

namespace Tests.Roles;

public class RoleTests
{
    [Fact]
    public void CannotSetAdminRole()
    {
        var role = new Role(BaseRoleNames.Ceo, new List<Permission>());
        var exception = Assert.Throws<RoleOperationException>(() => role.Update(BaseRoleNames.Admin, new List<Permission>()));
        Assert.Equal("Can't set admin role", exception.Message);
    }

    [Fact]
    public void CannotUpdateRoleWithIncorrectPermissions()
    {
        var incorrectPermissions = new List<Permission>
        {
            new(Permissions.EditFullEmployeesData),
        };

        var role = new Role(BaseRoleNames.Ceo, new List<Permission>());
        Assert.Throws<ArgumentException>(() => role.Update(BaseRoleNames.Ceo, incorrectPermissions));
    }

    [Fact]
    public void CannotCreateRoleWithIncorrectPermissions()
    {
        var incorrectPermissions = new List<Permission>
        {
            new(Permissions.EditFullEmployeesData),
        };

        Assert.Throws<ArgumentException>(() => new Role(BaseRoleNames.Ceo, incorrectPermissions));
        Assert.Throws<ArgumentException>(() => new Role(It.IsAny<long>(), BaseRoleNames.Ceo, incorrectPermissions));
    }

    [Fact]
    public void CanCreateRoleIfPermissionsAreCorrect()
    {
        var incorrectPermissions = new List<Permission>
        {
            new(Permissions.ViewContacts),
            new(Permissions.ViewSalaryAndDocumentsData),
            new(Permissions.EditFullEmployeesData),
        };

        Assert.Null(Record.Exception(() => new Role(BaseRoleNames.Ceo, incorrectPermissions)));
        Assert.Null(Record.Exception(() => new Role(It.IsAny<long>(), BaseRoleNames.Ceo, incorrectPermissions)));
    }
}