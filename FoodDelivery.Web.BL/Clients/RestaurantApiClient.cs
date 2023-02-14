namespace FoodDelivery.Web.BL.Clients;

public partial class RestaurantApiClient
{

    public RestaurantApiClient(AuthenticatedHttpClient authenticatedHttpClient)
    {
        _httpClient = authenticatedHttpClient.HttpClient;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
    }
}
