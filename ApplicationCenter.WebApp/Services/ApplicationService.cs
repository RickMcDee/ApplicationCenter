using ApplicationCenter.Shared.Models;
using System.Text.Json;

namespace ApplicationCenter.WebApp.Services;

public class ApplicationService(IHttpClientFactory factory)
{
    private readonly HttpClient _backendClient = factory.CreateClient("BackendClient");
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private const string _applicationsEndpoint = "applications";

    public async Task<List<ApplicationViewModel>> GetApplications()
    {
        var response = await _backendClient.GetFromJsonAsync<List<ApplicationViewModel>>(_applicationsEndpoint) ?? throw new Exception("Received empty response");
        return response;
    }

    public async Task<ApplicationViewModel> AddApplication(ApplicationViewModel application)
    {
        var response = await _backendClient.PostAsJsonAsync(_applicationsEndpoint, application);
        response.EnsureSuccessStatusCode();

        var contentString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApplicationViewModel>(contentString, _serializerOptions) ?? throw new Exception("Received empty response");

        return result;
    }

    public async Task RemoveApplication(Guid applicationId)
    {
        var response = await _backendClient.DeleteAsync($"{_applicationsEndpoint}/{applicationId}");
        response.EnsureSuccessStatusCode();
    }
}
