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

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private OrderFacade OrderFacade { get; set; } = null!;

    private OrderDetailModel Data { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Data = await OrderFacade.GetByIdAsync(Id);
            await base.OnInitializedAsync();
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }

    public async Task Save()
    {
        try
        {
            if (Data.OrderState == OrderState.Delivered)
                Data.DeliveryTime = DateTime.Now;
            await OrderFacade.SaveToApi(Data);
            NavigationManager.NavigateTo($"/orders");
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }

    public async Task Delete()
    {
        try
        {
            await OrderFacade.DeleteAsync(Id);
            NavigationManager.NavigateTo($"/orders");
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}