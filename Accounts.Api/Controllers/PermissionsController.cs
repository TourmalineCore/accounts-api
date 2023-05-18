using Accounts.Application.Permissions;
using Accounts.Application.Permissions.Commands;
using Accounts.Application.Permissions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("api/permissions")]
public class PermissionsController : Controller
{
    private readonly GetPermissionsQueryHandler _getPermissionsQueryHandler;
    private readonly GetPermissionByIdQueryHandler _getPermissionByIdQueryHandler;
    private readonly DeletePermissionCommandHandler _deletePermissionCommandHandler;
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;

    public PermissionsController(
        GetPermissionsQueryHandler getPermissionsQueryHandler,
        DeletePermissionCommandHandler deletePermissionCommandHandler,
        GetPermissionByIdQueryHandler getPermissionByIdQueryHandler,
        GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler)
    {
        _getPermissionsQueryHandler = getPermissionsQueryHandler;
        _deletePermissionCommandHandler = deletePermissionCommandHandler;
        _getPermissionByIdQueryHandler = getPermissionByIdQueryHandler;
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
    }

    [HttpGet("all")]
    public Task<IEnumerable<PermissionDto>> FindAll([FromQuery] GetPermissionsQuery getPermissionsQuery)
    {
        return _getPermissionsQueryHandler.Handle(getPermissionsQuery);
    }

    [HttpGet("find/{Id}")]
    public Task<PermissionDto> FindById([FromRoute] GetPermissionByIdQuery getPermissionsByIdQuery)
    {
        return _getPermissionByIdQueryHandler.Handle(getPermissionsByIdQuery);
    }

    [HttpGet("getByAccountId/{accountId}")]
    public Task<IEnumerable<string>> FindByAccountId([FromRoute] long accountId)
    {
        return _getPermissionsByAccountIdQueryHandler.Handle(accountId);
    }

    [HttpDelete("delete")]
    public Task Delete([FromQuery] DeletePermissionCommand deletePermissionCommand)
    {
        return _deletePermissionCommandHandler.Handle(deletePermissionCommand);
    }
}