using System.Threading.Tasks;

namespace Application.HttpClients;

public interface IHttpClient
{
    Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail, string token);

    Task SendRequestToCreateNewEmployeeAsync(string corporateEmail, string firstName, string lastName, string? middleName, long tenantId);

    Task SendRequestToBlockUserAsync(long accountId);

    Task SendRequestToUnblockUserAsync(long accountId);
}