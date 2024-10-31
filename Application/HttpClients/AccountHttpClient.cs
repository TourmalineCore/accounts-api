using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Application.HttpClients;

public class AccountHttpClient : IHttpClient
{
    private readonly HttpClient _client;
    private readonly HttpUrls _urls;

    public AccountHttpClient(IOptions<HttpUrls> urls)
    {
        _client = new HttpClient();
        _urls = urls.Value;
    }

    public async Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail, string token)
    {
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        var response = await _client.PostAsJsonAsync($"{_urls.AuthServiceUrl}/api/auth/register",
                new
                {
                    AccountId = accountId,
                    CorporateEmail = corporateEmail,
                }
            );
        System.Console.WriteLine("************ REGISTER access token: " + token);
        System.Console.WriteLine("******** Register result: ");
        System.Console.WriteLine("*** statuscode:" + response.StatusCode);
        System.Console.WriteLine("*** content:" + response.Content.ToString());
    }

    public async Task SendRequestToCreateNewEmployeeAsync(string corporateEmail, string firstName, string lastName, string? middleName, long tenantId)
    {
        await _client.PostAsJsonAsync($"{_urls.EmployeeServiceUrl}/internal/create-employee",
                new
                {
                    CorporateEmail = corporateEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    TenantId = tenantId
                }
            );
    }

    public async Task SendRequestToBlockUserAsync(long accountId)
    {
        await _client.PostAsJsonAsync($"{_urls.AuthServiceUrl}/internal/block-user",
                new
                {
                    AccountId = accountId,
                }
            );
    }

    public async Task SendRequestToUnblockUserAsync(long accountId)
    {
        await _client.PostAsJsonAsync($"{_urls.AuthServiceUrl}/internal/unblock-user",
                new
                {
                    AccountId = accountId,
                }
            );
    }
}