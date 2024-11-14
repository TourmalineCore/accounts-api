using Application.Roles;
using Application.Roles.Commands;
using Application.Roles.Queries;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers;

[Authorize]
[Route("api/roles")]
public class RolesController : Controller
{
    private readonly GetRolesQueryHandler _getRolesQueryHandler;
    private readonly RoleCreationCommandHandler _roleCreationCommandHandler;
    private readonly RoleUpdateCommandHandler _roleUpdateCommandHandler;
    private readonly GetRoleByIdQueryHandler _getRoleByIdQueryHandler;
    private readonly RoleRemoveCommandHandler _roleRemoveCommandHandler;

    public RolesController(
        GetRolesQueryHandler getRolesQueryHandler,
        GetRoleByIdQueryHandler getRoleByIdQueryHandler,
        RoleRemoveCommandHandler roleRemoveCommandHandler,
        RoleCreationCommandHandler roleCreationCommandHandler,
        RoleUpdateCommandHandler roleUpdateCommandHandler)
    {
        _getRolesQueryHandler = getRolesQueryHandler;
        _getRoleByIdQueryHandler = getRoleByIdQueryHandler;
        _roleRemoveCommandHandler = roleRemoveCommandHandler;
        _roleCreationCommandHandler = roleCreationCommandHandler;
        _roleUpdateCommandHandler = roleUpdateCommandHandler;
    }

    [RequiresPermission(Permissions.ViewRoles)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllAsync()
    {
        try
        {
            var roles = await _getRolesQueryHandler.HandleAsync();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ViewRoles)]
    [HttpGet("find/{roleId:long}")]
    public async Task<ActionResult<RoleDto>> GetByIdAsync(long roleId)
    {
        try
        {
            var role = await _getRoleByIdQueryHandler.HandleAsync(new GetRoleByIdQuery
            {
                Id = roleId,
            }
                );

            return Ok(role);
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageRoles)]
    [HttpPost("create")]
    public async Task<ActionResult> CreateNewRoleAsync([FromBody] RoleCreationCommand roleCreationCommand)
    {
        try
        {
            await _roleCreationCommandHandler.HandleAsync(roleCreationCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageRoles)]
    [HttpPost("edit")]
    public async Task<ActionResult> UpdateRoleAsync([FromBody] RoleUpdateCommand roleUpdateCommand)
    {
        try
        {
            await _roleUpdateCommandHandler.HandleAsync(roleUpdateCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageRoles)]
    [HttpDelete("{roleId:long}")]
    public async Task<ActionResult> RemoveAsync(long roleId)
    {
        try
        {
            await _roleRemoveCommandHandler.HandleAsync(new RoleRemoveCommand
            {
                Id = roleId,
            }
                );

            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    private ObjectResult GetProblem(Exception exception)
    {
        return Problem(exception.Message, nameof(RolesController), StatusCodes.Status500InternalServerError);
    }
}