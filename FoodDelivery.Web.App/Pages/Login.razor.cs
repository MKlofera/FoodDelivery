using FoodDelivery.Common.Models.Models.User;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using FoodDelivery.Common.Models.Models.Auth;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

namespace FoodDelivery.Web.App.Pages;

public partial class Login
{

    public UserCredentialsModel UserCredentials { get; set; } = new();

    private string InMemoryToken { get; set; }

    [Inject]
    private IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    [Inject]
    public ILocalStorageService LocalStorageService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private async Task GetToken()
    {
        var client = HttpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var result = await client.PostAsync("https://localhost:7176/api/auth/token-jwt", new StringContent(
            JsonSerializer.Serialize(new UserCredentialsModel()
            {
                Login = UserCredentials.Login,
                Password = UserCredentials.Password
            }), Encoding.UTF8, "application/json"));

        var data = await result.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<AuthTokenModel>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if (token is not null)
        {
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");

            await LocalStorageService.SetItemAsStringAsync("access-token", token.AccessToken);
        }

        NavigationManager.NavigateTo("/");
    }


}