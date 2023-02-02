using FoodDelivery.Common;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class OrderEditPage
{
    [Parameter] public Guid Id { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject] private OrderFacade OrderFacade { get; set; } = null!;

    private OrderDetailModel Data { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Data = await OrderFacade.GetByIdAsync(Id);
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        if (Data.OrderState == OrderState.Delivered)
            Data.DeliveryTime = DateTime.Now;
        await OrderFacade.SaveToApi(Data);
        navigationManager.NavigateTo($"/orders");
    }

    public async Task Delete()
    {
        await OrderFacade.DeleteAsync(Id);
        navigationManager.NavigateTo($"/orders");
    }
}
