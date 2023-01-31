using System.Threading.Tasks;

namespace Accounts.Application.HttpClients
{
    public interface IHttpClient
    {
        Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail);
    }
}
