using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace FoodDelivery.Web.App.Pages;

public partial class RestaurantSalesPage
{
    [Inject] private RestaurantFacade RestaurantFacade { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    ICollection<RestaurantSalesModel>? sales;

    GridSort<RestaurantSalesModel> alphabeticalSort =
        GridSort<RestaurantSalesModel>.ByAscending(s => s.Restaurant.Name);

    protected override async Task OnInitializedAsync()
    {
        try
        {
            sales = await RestaurantFacade.GetAllSalesAsync();
            await base.OnInitializedAsync();
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}