using Accounts.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Application.Privileges.Queries
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

        public async Task<IEnumerable<string>> Handle(long accountId)
        {
            var user = await _userRepository.FindByIdAsync(accountId);

            return user.AccountRoles
                .Select(x => x.Role)
                .SelectMany(x => x.Privileges)
                .Select(x => x.Name.ToString());
        }
    }
}
