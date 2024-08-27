using System.Threading.Tasks;
using Core.Contracts;

namespace Application.Tenants.Commands;

public class TenantDeleteCommandHandler
{
    private readonly ITenantsRepository _tenantRepository;

    public TenantDeleteCommandHandler(ITenantsRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task HandleAsync(long id)
    {
        var tenant = await _tenantRepository.GetByIdAsync(id);
        await _tenantRepository.RemoveAsync(tenant);
    }
}
