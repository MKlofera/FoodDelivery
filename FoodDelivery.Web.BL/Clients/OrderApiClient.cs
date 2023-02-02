namespace FoodDelivery.Web.BL.Clients;

public partial class OrderApiClient
{
    public OrderApiClient(HttpClient httpClient, string? baseUrl) : this(baseUrl, httpClient)
    {
        BaseUrl = baseUrl;
    }
}
