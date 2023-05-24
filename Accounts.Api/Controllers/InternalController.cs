using Accounts.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("internal")]
public class InternalController : Controller
{
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;

    public InternalController(GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler)
    {
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
    }

    [HttpGet("account-permissions/{accountId:long}")]
    public Task<IEnumerable<string>> GetPermissionsByAccountIdAsync([FromRoute] long accountId)
    {
        return _getPermissionsByAccountIdQueryHandler.Handle(accountId);
    }
}