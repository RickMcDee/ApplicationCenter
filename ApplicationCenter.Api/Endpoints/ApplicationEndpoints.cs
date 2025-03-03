using ApplicationCenter.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCenter.Api.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("Applications")
            //.RequireAuthorization()
            .WithOpenApi();

        group.MapGet("/", GetApplications);
    }

    private static async Task<IResult> GetApplications([FromServices] IApplicationService service)
    {
        var result = await service.GetApplications();

        return Results.Ok(result);
    }
}
