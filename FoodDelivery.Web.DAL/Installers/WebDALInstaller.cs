using FoodDelivery.Common.Installers;
using FoodDelivery.Web.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Web.DAL.Installers
{
    public class WebDALInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LocalDb>();
            serviceCollection.Scan(scan =>
                scan.FromAssemblyOf<WebDALInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IWebRepository<>)))
                    .AsSelf()
                    .WithSingletonLifetime());
        }
    }
}
