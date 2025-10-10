using Application.Roles.Commands;
using Core.Contracts;
using Core.Entities;
using Moq;
using Tests.TestsData;

namespace Tests.Roles;

public class RoleUpdateCommandTests
{
  private readonly Mock<IRolesRepository> _roleRepositoryMock = new();
  private readonly Role _ceoRole = TestData.AllRoles.Single(x => x.Name == TestData.RoleNames.Ceo);

  public RoleUpdateCommandTests()
  {
    _roleRepositoryMock
      .Setup(x => x.GetAllAsync())
      .ReturnsAsync(TestData.AllRoles);
  }

  [Fact]
  public async Task CanUpdateRoleWithTheSameParams()
  {
    var command = new RoleUpdateCommand
    {
      Id = _ceoRole.Id,
      Name = _ceoRole.Name,
      Permissions = _ceoRole.Permissions.ToList(),
    };

    _roleRepositoryMock
      .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
      .ReturnsAsync(_ceoRole);

    var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
    var exception = await Record.ExceptionAsync(() => roleUpdateCommandHandler.HandleAsync(command));

    Assert.Null(exception);
  }

  [Fact]
  public async Task CannotUpdateRoleIfNameIsNotUnique()
  {
    var command = new RoleUpdateCommand
    {
      Id = _ceoRole.Id,
      Name = TestData.RoleNames.Manager,
      Permissions = _ceoRole.Permissions.ToList(),
    };

    _roleRepositoryMock
      .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
      .ReturnsAsync(_ceoRole);

    var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleUpdateCommandHandler.HandleAsync(command));

    Assert.Equal($"Role with name [{TestData.RoleNames.Manager}] already exists", exception.Message);
  }

  [Fact]
  public async Task CannotUpdateRoleWithNonExistingPermissions()
  {
    var command = new RoleUpdateCommand
    {
      Id = _ceoRole.Id,
      Name = _ceoRole.Name,
      Permissions = new List<string>
      {
        "NonExistingPermission",
      },
    };

    _roleRepositoryMock
      .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
      .ReturnsAsync(_ceoRole);

    var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
    var exception = await Assert.ThrowsAsync<ArgumentException>(() => roleUpdateCommandHandler.HandleAsync(command));

    Assert.Equal("Permission [NonExistingPermission] doesn't exists", exception.Message);
  }

  [Fact]
  public async Task CannotUpdateRoleIfDoesNotExists()
  {
    const int nonExistingRoleId = 1000;

    var command = new RoleUpdateCommand
    {
      Id = nonExistingRoleId,
      Name = _ceoRole.Name,
      Permissions = _ceoRole.Permissions.ToList(),
    };

    var roleUpdateCommandHandler = new RoleUpdateCommandHandler(_roleRepositoryMock.Object);
    await Assert.ThrowsAsync<NullReferenceException>(() => roleUpdateCommandHandler.HandleAsync(command));
  }
}
