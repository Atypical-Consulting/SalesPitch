using Betalgo.Ranul.OpenAI.Extensions;
using MudBlazor.Services;
using SalesPitch.WebApp.Components;
using SalesPitch.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add OpenAI services
builder.Services.AddOpenAIService(settings =>
{
    settings.ApiKey = builder.Configuration["OpenAIServiceOptions:ApiKey"] ?? 
                     throw new InvalidOperationException("OpenAI API key not configured");
});

// Add application services
builder.Services.AddScoped<ISalesPitchService, SalesPitchService>();
builder.Services.AddSingleton<ThemeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();