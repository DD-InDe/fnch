using EAS_Web.Components;
using EAS_Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

app.UseExceptionHandler("/Error", createScopeForErrors: true);

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();