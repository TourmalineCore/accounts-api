using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Application.Users.Queries
{
    public class GetUserByIdQuery
    {
        public long Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request)
        {
            var userEntity = await _userRepository.FindByIdAsync(request.Id);

            // ToDo получать привилегии из всех ролей
            var userPrivileges = userEntity.Roles[0].Privileges.Select(x => x.Name).Select(n => n.ToString());

            return new UserDto(userEntity.Id, userEntity.Email, userEntity.RoleId, userPrivileges);
        }
    }
}
