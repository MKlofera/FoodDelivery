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
            serviceCollection.AddTransient<IRestaurantApiClient, RestaurantApiClient>();
            serviceCollection.AddTransient<IOrderApiClient, OrderApiClient>();
            serviceCollection.AddTransient<IFoodApiClient, FoodApiClient>();
            serviceCollection.AddTransient<ISearchApiClient, SearchApiClient>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }

    }
}