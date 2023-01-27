using NodaTime;

namespace UserManagementService.Core.Entities
{
    public class User : IIdentityEntity
    {
        public long Id { get; private set; }

        public string Email { get; private set; }

        public long RoleId { get; private set; }

        public Role Role { get; private set; }
        public Instant? DeletedAtUtc { get; private set; } = null;

        // For DB Context
        private User() { }

        public User(string email, long roleId)
        {
            Email = email;
            RoleId = roleId;
        }

        public void Update(string email, long roleId)
        {
            Email = email;
            RoleId = roleId;
        }

        public void AddRole(Role role)
        {
            Role = role;
        }
        public void Delete(Instant deletedAtUtc)
        {
            DeletedAtUtc = deletedAtUtc;
        }
    }
}
