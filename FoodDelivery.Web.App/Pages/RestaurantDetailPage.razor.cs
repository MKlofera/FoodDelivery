using System.Xml.Linq;
using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FoodDelivery.Web.App.Pages;

public partial class RestaurantDetailPage
{
    [Parameter] public Guid Id { get; set; }

    [Inject] private RestaurantFacade RestaurantFacade { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private RestaurantDetailModel Restaurant { get; set; } = new();
    public ICollection<FoodListModel>? FoodList { get; set; } = new List<FoodListModel>();
    private ICollection<OrderListModel>? OrderList { get; set; } = new List<OrderListModel>();
    private double? RestaurantSales { get; set; } = 0;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Restaurant = await RestaurantFacade.GetByIdAsync(Id);
            FoodList = await RestaurantFacade.GetFoodsAsync(Id);
            OrderList = await RestaurantFacade.GetOrdersAsync(Id);
            RestaurantSales = await RestaurantFacade.GetSalesAsync(Id);
            await base.OnInitializedAsync();
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}