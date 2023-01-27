using System.Threading.Tasks;

namespace Accounts.Application.HttpClients
{
    public interface IHttpClient
    {
        Task SendDataAuthApi(long accountId, string corporateEmail);
    }
}
