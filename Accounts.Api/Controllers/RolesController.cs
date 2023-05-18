using Accounts.Application.Roles;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Roles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("api/roles")]
public class RolesController : Controller
{
    private readonly GetRoleListQueryHandler _getRoleListQueryHandler;
    private readonly GetRoleByIdQueryHandler _getRoleByIdQueryHandler;
    private readonly DeleteRoleCommandHandler _deleteRoleCommandHandler;
    private readonly AddPermissionCommandHandler _addPermissionCommandhandler;

    public RolesController(
        GetRoleListQueryHandler getRoleListQueryHandler,
        GetRoleByIdQueryHandler getRoleByIdQueryHandler,
        DeleteRoleCommandHandler deleteRoleCommandHandler,
        AddPermissionCommandHandler addPermissionCommandHandler)
    {
        _getRoleListQueryHandler = getRoleListQueryHandler;
        _getRoleByIdQueryHandler = getRoleByIdQueryHandler;
        _deleteRoleCommandHandler = deleteRoleCommandHandler;
        _addPermissionCommandhandler = addPermissionCommandHandler;
    }

    [HttpGet]
    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        return await _getRoleListQueryHandler.Handle();
    }

    [HttpGet("find/{id}")]
    public Task<RoleDto> FindById([FromRoute] GetRoleByIdQuery getRoleByIdQuery)
    {
        return _getRoleByIdQueryHandler.Handle(getRoleByIdQuery);
    }

    [HttpGet("delete")]
    public Task FindById([FromQuery] DeleteRoleCommand deleteRoleCommand)
    {
        return _deleteRoleCommandHandler.Handle(deleteRoleCommand);
    }

    [HttpPost("add-permission")]
    public Task AddPermissionAsync([FromBody] AddPermissionCommand addPermissionCommand)
    {
        return _addPermissionCommandhandler.Handle(addPermissionCommand);
    }
}