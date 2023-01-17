using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Application.HttpClients
{
    public class AccountHttpClient
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
