using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Accounts.Application.HttpClients
{
    public class AccountHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly HttpUrls _urls;
        public AccountHttpClient(IOptions<HttpUrls> urls)
        {
            _client = new HttpClient();
            _urls = urls.Value;
        }
        public async Task SendDataAuthApi(long accountId, string corporateEmail)
        {
            string url = $"{_urls.AuthServiceUrl}api/auth/register";

            await _client.PostAsJsonAsync(url,
                new
                {
                    AccountId = accountId,
                    CorporateEmail = corporateEmail,
                });

        }
    }
}
