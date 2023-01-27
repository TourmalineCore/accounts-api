using Accounts.Core.Entities;
using System.Collections.Generic;

namespace Accounts.Application.Users
{
    public class UserDto
    {
        public UserDto()
        {
        }

        public UserDto(
            long id,
            string email,
            long roleId,
            IEnumerable<string> privileges)
        {
            Id = id;
            Email = email;
            RoleId = roleId;
            Privileges = privileges;
        }

        public UserDto(
            long id,
            string email,
            long roleId)
        {
            Id = id;
            Email = email;
            RoleId = roleId;
        }

        public long Id { get; private set; }

        public string Email { get; private set; }

        public long RoleId { get; private set; }

        public IEnumerable<string> Privileges { get; private set; }
    }
}
