using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Entities;
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
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_urls.AuthServiceUrl}/api/auth/register")
        {
            Content = JsonContent.Create(new
            {
                AccountId = accountId,
                CorporateEmail = corporateEmail,
            })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        await _client.SendAsync(request);
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

    public async Task SendRequestToUpdateEmployeePersonalInfoAsync(string corporateEmail, string firstName, string lastName, string? middleName)
    {
        await _client.PostAsJsonAsync($"{_urls.EmployeeServiceUrl}/internal/update-employee-personal-info",
                new
                {
                    CorporateEmail = corporateEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName
                }
            );
    }
    public async Task SendRequestToDeleteAccountAsync(string corporateEmail, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_urls.AuthServiceUrl}/api/auth/delete-user")
        {
            Content = JsonContent.Create(new
            {
                CorporateEmail = corporateEmail
            })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _client.SendAsync(request);
    }
    public async Task SendRequestToDeleteEmployeeAsync(string corporateEmail, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_urls.EmployeeServiceUrl}/internal/delete-employee")
        {
            Content = JsonContent.Create(new
            {
                CorporateEmail = corporateEmail
            })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _client.SendAsync(request);
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