using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;

namespace Accounts.Application.Privileges.Queries
{
    public class GetPrivilegeByIdQuery
    {
        public long Id { get; set; }
    }
    public class GetPrivilegeByIdQueryHandler : IQueryHandler<GetPrivilegeByIdQuery, PrivilegeDto>
    {
        private readonly IPrivilegeRepository _privilegeRepository;

        public GetPrivilegeByIdQueryHandler(IPrivilegeRepository privilegeRepository)
        {
            _privilegeRepository = privilegeRepository;
        }

        public async Task<PrivilegeDto> Handle(GetPrivilegeByIdQuery request)
        {
            var privilegeEntity = await _privilegeRepository.FindByIdAsync(request.Id);

            return new PrivilegeDto(privilegeEntity.Id, privilegeEntity.Name.ToString());
        }
    }
}
