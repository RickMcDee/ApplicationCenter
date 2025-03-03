using ApplicationCenter.Shared.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace ApplicationCenter.WebApp.Services;

public class ConfigurationKeyService(IHttpClientFactory factory)
{
    private readonly HttpClient _backendClient = factory.CreateClient("BackendClient");
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private const string _configurationKeysEndpoint = "configurationkeys";

    public async Task<List<ConfigurationKeyViewModel>> GetConfigurationKeys(Guid applicationId)
    {
        var parameters = new Dictionary<string, string?>
        {
            ["applicationId"] = applicationId.ToString(),
        };

        var queryString = QueryHelpers.AddQueryString(_configurationKeysEndpoint, parameters);
        var response = await _backendClient.GetFromJsonAsync<List<ConfigurationKeyViewModel>>(queryString) ?? throw new Exception("Received empty response");
        return response;
    }

    public async Task<ConfigurationKeyViewModel> AddConfigurationKey(ConfigurationKeyViewModel configurationKey, Guid applicationId)
    {
        var parameters = new Dictionary<string, string?>
        {
            ["applicationId"] = applicationId.ToString(),
        };

        var queryString = QueryHelpers.AddQueryString(_configurationKeysEndpoint, parameters);
        var response = await _backendClient.PostAsJsonAsync(queryString, configurationKey);
        response.EnsureSuccessStatusCode();

        var contentString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ConfigurationKeyViewModel>(contentString, _serializerOptions) ?? throw new Exception("Received empty response");

        return result;
    }

    public async Task RemoveConfigurationKey(Guid configurationKeyId)
    {
        var response = await _backendClient.DeleteAsync($"{_configurationKeysEndpoint}/{configurationKeyId}");
        response.EnsureSuccessStatusCode();
    }
}
