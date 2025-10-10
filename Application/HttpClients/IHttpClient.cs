using System.Threading.Tasks;

namespace Application.HttpClients;

public interface IHttpClient
{
  Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail, string token);

  Task SendRequestToCreateNewEmployeeAsync(string corporateEmail, string firstName, string lastName, string? middleName, long tenantId);

  Task SendRequestToDeleteAccountAsync(string corporateEmail, string token);

  Task SendRequestToDeleteEmployeeAsync(string corporateEmail, string token);

  Task SendRequestToBlockUserAsync(long accountId);

  Task SendRequestToUnblockUserAsync(long accountId);

  Task SendRequestToUpdateEmployeePersonalInfoAsync(string corporateEmail, string firstName, string lastName, string? middleName);
}
