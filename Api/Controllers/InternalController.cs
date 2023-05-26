using Application.Accounts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("internal")]
public class InternalController : Controller
{
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;

    public InternalController(GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler)
    {
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
    }

    [HttpGet("account-permissions/{accountId:long}")]
    public async Task<IEnumerable<string>> GetPermissionsByAccountIdAsync(long accountId)
    {
        return await _getPermissionsByAccountIdQueryHandler.HandleAsync(new GetPermissionsByAccountIdQuery
                {
                    Id = accountId,
                }
            );
    }
}