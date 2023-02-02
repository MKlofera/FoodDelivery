using FoodDelivery.Common.BL;
using FoodDelivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using FoodDelivery.Web.BL.Clients;

namespace FoodDelivery.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection, string? apiBaseUrl)
        {
            serviceCollection.AddTransient<IRestaurantApiClient, RestaurantApiClient>(provider =>
            {
                var client = CreateApiHttpClient(apiBaseUrl);
                return new RestaurantApiClient(client, apiBaseUrl);
            });
            serviceCollection.AddTransient<IOrderApiClient, OrderApiClient>(provider =>
            {
                var client = CreateApiHttpClient(apiBaseUrl);
                return new OrderApiClient(client, apiBaseUrl);
            });
            serviceCollection.AddTransient<IFoodApiClient, FoodApiClient>(provider =>
            {
                var client = CreateApiHttpClient(apiBaseUrl);
                return new FoodApiClient(client, apiBaseUrl);
            });
            serviceCollection.AddTransient<ISearchApiClient, SearchApiClient>(provider =>
            {
                var client = CreateApiHttpClient(apiBaseUrl);
                return new SearchApiClient(client, apiBaseUrl);
            });

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }


        public HttpClient CreateApiHttpClient(string? apiBaseUrl)
        {
            var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
            client.BaseAddress = new Uri(apiBaseUrl);
            return client;
        }
    }
}