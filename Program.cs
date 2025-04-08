using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Slangespillet;
using Slangespillet.Game.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Load appsettings.json (WASM automatically loads it)
builder.Services.AddScoped<LeaderboardService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var config = sp.GetRequiredService<IConfiguration>();
    return new LeaderboardService(httpClient, config);
});


await builder.Build().RunAsync();
