using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Roles.Queries;

public abstract class GetRoleListQuery
{
}

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, IEnumerable<RoleDto>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleListQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetRoleListQuery? request = null)
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(role => new RoleDto(role)).OrderBy(x => x.Id);
    }
}