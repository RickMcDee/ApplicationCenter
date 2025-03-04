using Microsoft.AspNetCore.Mvc;

namespace ApplicationCenter.Api.Endpoints;

public static class ConfigurationKeyEndpoints
{
    public static void MapConfigurationKeyEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("ConfigurationKeys")
            //.RequireAuthorization()
            .WithOpenApi();

        group.MapPost("/", AddOrUpdateConfigurationKey);
        group.MapGet("/", GetConfigurationKeys);
        group.MapDelete("/{configurationKeyId}", RemoveConfigurationKey);
    }

    private static async Task<IResult> AddOrUpdateConfigurationKey([FromServices] IConfigurationKeyService service, [FromBody] ConfigurationKeyViewModel configurationKey, [FromQuery] Guid? applicationId)
    {
        var newModel = await service.AddOrUpdateConfigurationKey(configurationKey, applicationId);

        return Results.Ok(newModel);
    }

    private static async Task<IResult> GetConfigurationKeys([FromServices] IConfigurationKeyService service, [FromQuery] Guid applicationId)
    {
        var result = await service.GetConfigurationKeys(applicationId);

        return Results.Ok(result);
    }

    private static async Task<IResult> RemoveConfigurationKey([FromServices] IConfigurationKeyService service, [FromRoute] Guid configurationKeyId)
    {
        await service.RemoveConfigurationKey(configurationKeyId);

        return Results.NoContent();
    }
}
