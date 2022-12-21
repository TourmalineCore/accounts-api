using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Core.Contracts;

namespace UserManagementService.Application.Privileges.Queries
{
    public partial class GetPrivilegesByAccountIdQuery
    {
    }
    public class GetPrivilegesByAccountIdQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetPrivilegesByAccountIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PrivilegeDto>> Handle(long accountId)
        {
            var user = await _userRepository.FindByIdAsync(accountId);

            return user.Role.Privileges.Select(x => new PrivilegeDto(x.Id, x.Name.ToString()));
        }
    }
}
