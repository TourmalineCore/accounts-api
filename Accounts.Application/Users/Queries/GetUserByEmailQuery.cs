using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Queries
{
    public partial class GetUserByEmailQuery
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request)
        {
            var userEntity = await _userRepository.FindByEmailAsync(request.Email);

            if (userEntity == null)
            {
                return new UserDto();
            }

            // ToDo получать привилегии из всех ролей
            var userPrivileges = userEntity.Roles[0].Privileges.Select(x => x.Name).Select(n => n.ToString());

            return new UserDto(userEntity.Id, userEntity.Email, userEntity.RoleId, userPrivileges);
        }
    }
}
