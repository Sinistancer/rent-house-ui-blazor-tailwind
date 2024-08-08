using daryon_house_ui.Client;
using daryon_house_ui.Client.Application;
using daryon_house_ui.Client.BFF;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// authentication state plumbing
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();
builder.Services.AddScoped<ConverterService>();
builder.Services.AddSingleton<ToastService>();

// HTTP client configuration
builder.Services.AddTransient<AntiforgeryHandler>();

builder.Services.AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AntiforgeryHandler>();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

await builder.Build().RunAsync();
