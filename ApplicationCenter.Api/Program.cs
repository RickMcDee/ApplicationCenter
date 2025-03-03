using ApplicationCenter.Api.Database;
using ApplicationCenter.Api.Endpoints;
using ApplicationCenter.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IApplicationService, ApplicationService>();

builder.Services.AddDbContextFactory<DatabaseContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    opt.UseNpgsql(connectionString);
#if DEBUG
    opt.EnableDetailedErrors();
    opt.EnableSensitiveDataLogging();
#endif
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(config =>
//    {
//        config.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            ValidateIssuer = false,
//            ValidateIssuerSigningKey = false,
//            ValidateLifetime = false,
//        };
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapApplicationEndpoints();

//app.UseAuthentication();
//app.UseAuthorization();

app.Run();