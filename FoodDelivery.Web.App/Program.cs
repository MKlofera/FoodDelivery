using System.Globalization;
using AutoMapper.Internal;
using Blazored.LocalStorage;
using FoodDelivery.Web.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FoodDelivery.Web.BL.Extensions;
using FoodDelivery.Web.BL.Installers;
using Microsoft.JSInterop;

const string defaultCulture = "cs";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string? apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAutoMapper(configuration =>
{
    configuration.Internal().MethodMappingEnabled = false;
}, typeof(WebBLInstaller));
builder.Services.AddLocalization();
builder.Services.AddBlazoredLocalStorage();
var host = builder.Build();

var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
var cultureString = (await jsRuntime.InvokeAsync<string>("blazorCulture.get")) ?? defaultCulture;

var culture = new CultureInfo(cultureString);
await jsRuntime.InvokeVoidAsync("blazorCulture.set", cultureString);

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
