using MercatorioAnalyzer;
using MercatorioAnalyzer.Components;
using MercatorioAnalyzer.Data.Client;
using MercatorioAnalyzer.Data.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MercatorioSqliteDBContext") ?? throw new InvalidOperationException("Connection string 'MercatorioSqliteDBContext' not found.");

builder.Services.AddDbContextFactory<MercatorioSqliteDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Add our PlayerContainer
builder.Services.AddScoped<PlayerContainer>();
//Add the storehouse buildings so we can use it later
builder.Services.AddScoped<Storehouse>();
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
    app.UseMigrationsEndPoint();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
