using Application.Tenants;
using Application.Tenants.Commands;
using Application.Tenants.Queries;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers;

[Authorize]
[Route("api/tenants")]
public class TenantsController : Controller
{
  private readonly TenantCreationCommandHandler _tenantCreationCommandHandler;
  private readonly TenantDeleteCommandHandler _tenantDeleteCommandHandler;
  private readonly GetTenantsQueryHandler _getTenantsQueryHandler;

  public TenantsController(
    TenantCreationCommandHandler tenantCreationCommandHandler,
    TenantDeleteCommandHandler tenantDeleteCommandHandler,
    GetTenantsQueryHandler getTenantsQueryHandler)
  {
    _tenantCreationCommandHandler = tenantCreationCommandHandler;
    _tenantDeleteCommandHandler = tenantDeleteCommandHandler;
    _getTenantsQueryHandler = getTenantsQueryHandler;
  }

  [RequiresPermission(Permissions.CanManageTenants)]
  [HttpGet("all")]
  public async Task<IEnumerable<TenantDto>> GetAllAsync()
  {
    return await _getTenantsQueryHandler.HandleAsync();
  }

  [RequiresPermission(Permissions.CanManageTenants)]
  [HttpPost]
  public async Task<long> CreateAsync([FromBody] TenantCreationCommand tenantCreationCommand)
  {
    return await _tenantCreationCommandHandler.HandleAsync(tenantCreationCommand);
  }

  [RequiresPermission(Permissions.IsTenantsHardDeleteAllowed, Permissions.CanManageTenants)]
  [HttpDelete("{id}")]
  public async Task DeleteAsync(long id)
  {
    await _tenantDeleteCommandHandler.HandleAsync(id);
  }
}
