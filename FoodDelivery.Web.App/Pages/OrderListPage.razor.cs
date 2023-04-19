using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Web.BL.Facades;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class OrderListPage
{
    [Inject] private OrderFacade OrderFacade { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    ICollection<OrderListModel>? orders;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            orders = (await OrderFacade.GetAllAsync())
                .OrderByDescending(o => o.CreatedDate)
                .ToList();
            await base.OnInitializedAsync();
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}
