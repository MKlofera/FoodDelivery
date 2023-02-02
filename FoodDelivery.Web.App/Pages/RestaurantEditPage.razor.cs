using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class RestaurantEditPage
{
	[Inject]
	private NavigationManager navigationManager { get; set; } = null!;

	[Inject]
	private RestaurantFacade RestaurantFacade { get; set; } = null!;

	private RestaurantDetailModel Data { get; set; } = new();

	[Parameter]
	public Guid Id { get; set; }

	protected override async Task OnInitializedAsync()
	{
		if (Id != Guid.Empty)
		{
			Data = await RestaurantFacade.GetByIdAsync(Id);
		}

		await base.OnInitializedAsync();
	}

	public async Task Save()
	{
		if (Id != Guid.Empty)
		{
			await RestaurantFacade.SaveToApi(Data);
		}
		else
		{
			await RestaurantFacade.InsertRestaurantAsync(Data);
		}

		navigationManager.NavigateTo($"/restaurants");
	}

	public async Task Delete()
	{
		await RestaurantFacade.DeleteAsync(Id);
		navigationManager.NavigateTo($"/restaurants");
	}
}
