using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;

namespace Accounts.Application.Permissions.Queries;

public class GetPermissionsQuery
{
}

public class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionsRepository _permissionsRepository;

    public GetPermissionsQueryHandler(IPermissionsRepository permissionsRepository)
    {
        _permissionsRepository = permissionsRepository;
    }

    public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery request)
    {
        var permission = await _permissionsRepository.GetAllAsync();
        return permission.Select(x => new PermissionDto(x.Id, x.Name.ToString()));
    }
}