using Microsoft.AspNetCore.Mvc;

namespace ApplicationCenter.Api.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("Applications")
            //.RequireAuthorization()
            .WithOpenApi();

        group.MapPost("/", AddOrUpdateApplication);
        group.MapGet("/", GetApplications);
        group.MapDelete("/{applicationId}", RemoveApplication);
    }

    private static async Task<IResult> AddOrUpdateApplication([FromServices] IApplicationService service, [FromBody] ApplicationViewModel application)
    {
        var newModel = await service.AddOrUpdateApplication(application);

        return Results.Ok(newModel);
    }

    private static async Task<IResult> GetApplications([FromServices] IApplicationService service)
    {
        var result = await service.GetApplications();

        return Results.Ok(result);
    }

    private static async Task<IResult> RemoveApplication([FromServices] IApplicationService service, [FromRoute] Guid applicationId)
    {
        await service.RemoveApplication(applicationId);

        return Results.NoContent();
    }
}
