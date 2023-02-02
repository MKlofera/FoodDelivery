using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class OrderDetailPage
{
    [Parameter] public Guid Id { get; set; }

    [Inject] private OrderFacade OrderFacade { get; set; } = null!;

    private OrderDetailModel Order { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Order = await OrderFacade.GetByIdAsync(Id);
        await base.OnInitializedAsync();
    }

    private decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var orderNote in Order.FoodOrderNotes)
        {
            totalPrice += orderNote.FoodPrice * orderNote.FoodQuantity;
        }
        return totalPrice;
    }
}
