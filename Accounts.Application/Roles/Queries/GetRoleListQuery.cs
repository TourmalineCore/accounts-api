using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Application.Roles.Queries
{
    public class GetRoleListQuery
    {
    }

    public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, IEnumerable<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleListQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetRoleListQuery request)
        {
            var roleEntities = await _roleRepository.GetAllAsync();

            return roleEntities.Select(role => new RoleDto(role));
        }
    }
}
