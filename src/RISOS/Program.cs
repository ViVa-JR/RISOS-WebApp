using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RISOS;
using RISOS.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddWebApplication(builder.Configuration);


var host = builder.Build();

var themeService = host.Services.GetRequiredService<ThemeStateService>();

await themeService.InitializeAsync();

await host.RunAsync();