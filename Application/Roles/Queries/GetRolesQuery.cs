using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Roles.Queries;

public class GetRolesQueryHandler : IQueryHandler<IEnumerable<RoleDto>>
{
    private readonly IRolesRepository _rolesRepository;

    public GetRolesQueryHandler(IRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task<IEnumerable<RoleDto>> HandleAsync()
    {
        var roles = await _rolesRepository.GetAllAsync();

        return roles
            .Select(role => new RoleDto(role))
            .OrderBy(x => x.Id);
    }
}