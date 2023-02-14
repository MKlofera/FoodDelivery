namespace FoodDelivery.Web.BL.Clients;

public class AuthenticatedHttpClient
{
    public HttpClient HttpClient { get; }

    public AuthenticatedHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}