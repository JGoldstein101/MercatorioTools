using MercatorioAnalyzer;
using MercatorioAnalyzer.Components;
using MercatorioAnalyzer.Data.Client;
using MercatorioAnalyzer.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Add our PlayerContainer
builder.Services.AddScoped<PlayerContainer>();
builder.Services.AddHttpClient<MercatorioApiClient>(client =>
{
    client.BaseAddress = new Uri("https://play.mercatorio.io/api/");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
