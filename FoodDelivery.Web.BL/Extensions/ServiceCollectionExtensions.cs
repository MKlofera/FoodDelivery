﻿using FoodDelivery.Web.BL.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Web.BL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection, string? apiBaseUrl)
        where TInstaller : WebBLInstaller, new()
    {
        var installer = new TInstaller();
        installer.Install(serviceCollection, apiBaseUrl);
    }
}