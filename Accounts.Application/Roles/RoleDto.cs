using Accounts.Core.Entities;

namespace Accounts.Application.Roles
{
    public readonly struct RoleDto
    {
        public RoleDto(Role role)
        {
            Id = role.Id;
            Name = role.NormalizedName;
        }

        public long Id { get; init; }

        public string Name { get; init; }
    }
}
