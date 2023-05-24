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
        public async Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail)
        {
            var url = $"{_urls.AuthServiceUrl}/register";

            await _client.PostAsJsonAsync(url,
                new
                {
                    AccountId = accountId,
                    CorporateEmail = corporateEmail,
                });

        }

        public async Task SendRequestToCreateNewEmployeeAsync(string corporateEmail, string firstName, string lastName, string? middleName)
        {
            var url = $"{_urls.EmployeeServiceUrl}/internal/create-employee";

            await _client.PostAsJsonAsync(url,
                new
                {
                    CorporateEmail = corporateEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                });
        }

        public async Task SendRequestToBlockUserAsync(long accountId)
        {
            var url = $"{_urls.AuthServiceUrl}/block";

            await _client.PostAsJsonAsync(url,
                new
                {
                    AccountId = accountId,
                });
        }

        public async Task SendRequestToUnblockUserAsync(long accountId)
        {
            var url = $"{_urls.AuthServiceUrl}/unblock";

            await _client.PostAsJsonAsync(url,
                new
                {
                    AccountId = accountId,
                });
        }
    }
}
