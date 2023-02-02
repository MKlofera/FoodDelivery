using FoodDelivery.Common;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using System.Collections;

namespace FoodDelivery.Web.App.Pages;

public partial class FoodEditPage
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    private RestaurantFacade RestaurantFacade { get; set; } = null!;

    [Inject]
    private FoodFacade FoodFacade { get; set; } = null!;

    private FoodDetailModel Data { get; set; } = new();

    private RestaurantDetailModel RestaurantData { get; set; } = new();

    public string? SelectedAllergenName;

    [Parameter]
    public Guid RestaurantId { get; set; }

    [Parameter]
    public Guid Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Id != Guid.Empty)
        {
            Data = await FoodFacade.GetByIdAsync(Id);

        }

        if (RestaurantId != Guid.Empty)
        {
            RestaurantData = await RestaurantFacade.GetByIdAsync(RestaurantId);

        }

        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        if (Id == Guid.Empty)
        {
            Data.RestaurantId = RestaurantId;
            await FoodFacade.InserFoodAsync(Data);
        }
        else
        {
            await FoodFacade.SaveToApiAsync(Data);
        }
        navigationManager.NavigateTo($"/restaurants");
    }

    public async Task Delete()
    {
        await FoodFacade.DeleteAsync(Id);
        navigationManager.NavigateTo($"/restaurants");
    }

    public void AddAllergen()
    {
        Allergen allergen = Enum.GetValues<Allergen>().First(t => t.ToString() == SelectedAllergenName);

        if (!Data.Allergens.Contains(allergen)) Data.Allergens.Add(allergen);
    }

    public void DeleteAllergen(Allergen allergen)
    {
        if (Data.Allergens.Contains(allergen)) Data.Allergens.Remove(allergen);
    }

}
