using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SocialMediaTesting;
using SocialMediaTesting.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Adds local services to the app
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();

// Adds http client to project for later injection to send http requests (not used when not calling apis)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();