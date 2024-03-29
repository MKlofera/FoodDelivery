using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class FoodDetailPage
{
    [Parameter] public Guid Id { get; set; }

    [Inject] private FoodFacade FoodFacade { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private FoodDetailModel Food { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Food = await FoodFacade.GetByIdAsync(Id);
            await base.OnInitializedAsync();
        }
        catch (Exception e)
        {
            NavigationManager.NavigateTo($"/login");
        }
    }
}