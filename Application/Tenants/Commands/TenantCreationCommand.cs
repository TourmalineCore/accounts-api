using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;

namespace Application.Tenants.Commands;

public class TenantCreationCommand
{
    public string Name { get; set; }
}

public class TenantCreationCommandHandler
{
    private readonly ITenantsRepository _tenantRepository;

    public TenantCreationCommandHandler(ITenantsRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task<long> HandleAsync(TenantCreationCommand command)
    {
        var tenant = new Tenant(command.Name);
        return await _tenantRepository.CreateAsync(tenant);
    }
}