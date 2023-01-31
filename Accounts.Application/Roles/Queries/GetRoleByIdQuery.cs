using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Roles.Queries
{
    public class GetRoleByIdQuery
    {
        public long Id { get; set; }
    }

    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request)
        {
            var role = await _roleRepository.FindByIdAsync(request.Id);

            return new RoleDto(role);
        }
    }
}
