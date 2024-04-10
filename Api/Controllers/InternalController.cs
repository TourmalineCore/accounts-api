using Application.Accounts.Queries;
using Application.Tenants.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("internal")]
public class InternalController : Controller
{
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;
    private readonly GetTenantByAccountIdQueryHandler _getTenantByAccountIdQueryHandler;

    public InternalController(GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler,
        GetTenantByAccountIdQueryHandler getTenantByAccountIdQueryHandler)
    {
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
        _getTenantByAccountIdQueryHandler = getTenantByAccountIdQueryHandler;
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

    [HttpGet("get-tenantId-by-accountId/{accountId:long}")]
    public async Task<long> GetTenantByAccountIdAsync(long accountId)
    {
        return await _getTenantByAccountIdQueryHandler.HandleAsync(new GetTenantByAccountIdQuery
        {
            AccountId = accountId,
        }
        );
    }
}