using ApplicationCenter.WebApp.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<ConfigurationKeyService>();
builder.Services.AddScoped<ConfigurationKeyValueService>();

builder.Services.AddDbContextFactory<DatabaseContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    opt.UseNpgsql(connectionString);
#if DEBUG
    opt.EnableDetailedErrors();
    opt.EnableSensitiveDataLogging();
#endif
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();