using Accounts.Application.Permissions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("api/permissions")]
public class PermissionsController : Controller
{
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;

    public PermissionsController(GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler)
    {
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
    }

    [HttpGet("getByAccountId/{accountId}")]
    public Task<IEnumerable<string>> GetPermissionsByAccountIdAsync([FromRoute] long accountId)
    {
        return _getPermissionsByAccountIdQueryHandler.Handle(accountId);
    }
}