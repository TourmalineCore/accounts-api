using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Contracts;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Accounts.Validators
{
    public class RolesValidator<T> : IAsyncPropertyValidator<T, List<long>>
    {
        private readonly IRolesRepository _rolesRepository;
        private string _errorMessage = "Role ids are incorrect";

        public RolesValidator(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public string Name => "RoleIdsValidator";

        public string GetDefaultMessageTemplate(string errorCode)
        {
            return _errorMessage;
        }

        public async Task<bool> IsValidAsync(ValidationContext<T> context, List<long> accountRoleIds, CancellationToken cancellation)
        {
            if (!accountRoleIds.Any())
            {
                _errorMessage = "Role ids can't be empty";
                return false;
            }

            if (!IsRoleIdsUnique(accountRoleIds))
            {
                _errorMessage = "Role ids have duplicates";
                return false;
            }

            var roles = await _rolesRepository.GetAllAsync();
            var roleIds = roles.Select(x => x.Id);

            var allGivenRolesExist = accountRoleIds.All(x => roleIds.Contains(x));

            if (allGivenRolesExist)
            {
                return true;
            }

            _errorMessage = "Incorrect role ids. Probably you tried to set unavailable role id";
            return false;
        }

        private static bool IsRoleIdsUnique(IReadOnlyCollection<long> roleIds)
        {
            var uniqueRoleIds = roleIds.Distinct().ToList();
            return roleIds.Count == uniqueRoleIds.Count;
        }
    }
}