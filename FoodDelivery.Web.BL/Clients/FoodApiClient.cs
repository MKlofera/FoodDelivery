namespace FoodDelivery.Web.BL.Clients;

public partial class FoodApiClient
{
    public FoodApiClient(HttpClient httpClient, string baseUrl) : this(baseUrl, httpClient)
    {
        BaseUrl = baseUrl;
    }
}