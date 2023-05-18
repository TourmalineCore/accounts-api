using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;

namespace Accounts.Application.Permissions.Queries;

public class GetPermissionByIdQuery
{
    public long Id { get; set; }
}

public class GetPermissionByIdQueryHandler : IQueryHandler<GetPermissionByIdQuery, PermissionDto>
{
    private readonly IPermissionsRepository _permissionsRepository;

    public GetPermissionByIdQueryHandler(IPermissionsRepository permissionsRepository)
    {
        _permissionsRepository = permissionsRepository;
    }

    public async Task<PermissionDto> Handle(GetPermissionByIdQuery request)
    {
        var permission = await _permissionsRepository.FindByIdAsync(request.Id);
        return new PermissionDto(permission.Id, permission.Name);
    }
}