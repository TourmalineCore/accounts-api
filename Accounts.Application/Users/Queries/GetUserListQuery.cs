using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Queries
{
    public partial class GetUserListQuery
    {
    }

    public class GetUserListQueryHandler : IQueryHandler<GetUserListQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUserListQuery request)
        {
            var userEntities = await _userRepository.GetAllAsync();
            return userEntities.Select(x => new UserDto(
                x.Id,
                x.Email,
                x.RoleId
                )
            );
        }
    }
}
