using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;

namespace Application.Tenants.Queries;

public class GetTenantsQueryHandler
{
    private readonly ITenantsRepository _tenantsRepository;

    public GetTenantsQueryHandler(ITenantsRepository tenantsRepository)
    {
        _tenantsRepository = tenantsRepository;
    }

    public async Task<List<TenantDto>> HandleAsync()
    {
        var tenants = await _tenantsRepository.GetAllAsync();

        return tenants
           .Select(tenant => new TenantDto { Id = tenant.Id, Name = tenant.Name })
           .ToList();
    }
}
