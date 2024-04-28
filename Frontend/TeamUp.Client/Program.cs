using BitzArt.Blazor.Cookies;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

using TeamUp.ApiLayer;
using TeamUp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.AddBlazorCookies();
builder.Services.AddFluentUIComponents();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddApiClient(builder.Configuration["BackendUrl"]!);

var app = builder.Build();

await app.RunAsync();
