using Core.Entities;
using Core.Exceptions;
using Core.Models;

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
}