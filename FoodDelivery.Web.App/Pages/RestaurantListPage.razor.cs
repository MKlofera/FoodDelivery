using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class RestaurantListPage
{
    [Inject] private RestaurantFacade RestaurantFacade { get; set; } = null!;

    ICollection<RestaurantListModel>? restaurants;

    protected override async Task OnInitializedAsync()
    {
        restaurants = await RestaurantFacade.GetAllAsync();
        await base.OnInitializedAsync();
    }
}
