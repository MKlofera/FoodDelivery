namespace FoodDelivery.Web.BL.Clients;

public partial class SearchApiClient
{
    public SearchApiClient(AuthenticatedHttpClient authenticatedHttpClient)
    {
        _httpClient = authenticatedHttpClient.HttpClient;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
    }
}