namespace FoodDelivery.Web.BL.Clients;

public partial class RestaurantApiClient
{
    public RestaurantApiClient(HttpClient httpClient, string? baseUrl) : this(baseUrl, httpClient)
    {
        BaseUrl = baseUrl;
    }
}
