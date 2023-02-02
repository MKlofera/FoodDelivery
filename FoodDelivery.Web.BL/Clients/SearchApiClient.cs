namespace FoodDelivery.Web.BL.Clients;

public partial class SearchApiClient
{
    public SearchApiClient(HttpClient httpClient, string baseUrl) : this(baseUrl, httpClient)
    {
        BaseUrl = baseUrl;
    }
}