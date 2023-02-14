namespace FoodDelivery.Web.BL.Clients;

public partial class OrderApiClient
{
    public OrderApiClient(AuthenticatedHttpClient authenticatedHttpClient)
    {
        _httpClient = authenticatedHttpClient.HttpClient;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);

    }
}
