using System.Net;
using Accounts.Application.Roles;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Roles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("api/roles")]
public class RolesController : Controller
{
    private readonly GetRoleListQueryHandler _getRoleListQueryHandler;
    private readonly RoleCreationCommandHandler _roleCreationCommandHandler;
    private readonly RoleUpdateCommandHandler _roleUpdateCommandHandler;
    private readonly GetRoleByIdQueryHandler _getRoleByIdQueryHandler;
    private readonly DeleteRoleCommandHandler _deleteRoleCommandHandler;

    public RolesController(
        GetRoleListQueryHandler getRoleListQueryHandler,
        GetRoleByIdQueryHandler getRoleByIdQueryHandler,
        DeleteRoleCommandHandler deleteRoleCommandHandler,
        RoleCreationCommandHandler roleCreationCommandHandler,
        RoleUpdateCommandHandler roleUpdateCommandHandler)
    {
        _getRoleListQueryHandler = getRoleListQueryHandler;
        _getRoleByIdQueryHandler = getRoleByIdQueryHandler;
        _deleteRoleCommandHandler = deleteRoleCommandHandler;
        _roleCreationCommandHandler = roleCreationCommandHandler;
        _roleUpdateCommandHandler = roleUpdateCommandHandler;
    }

    [HttpGet]
    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        return await _getRoleListQueryHandler.Handle();
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateNewRoleAsync([FromBody] RoleCreationCommand roleCreationCommand)
    {
        try
        {
            await _roleCreationCommandHandler.Handle(roleCreationCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(RolesController), (int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost("edit")]
    public async Task<ActionResult> UpdateRoleAsync([FromBody] RoleUpdateCommand roleUpdateCommand)
    {
        try
        {
            await _roleUpdateCommandHandler.Handle(roleUpdateCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(RolesController), (int)HttpStatusCode.InternalServerError);
        }
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
}