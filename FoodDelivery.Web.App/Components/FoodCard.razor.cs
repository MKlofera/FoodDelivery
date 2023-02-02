using FoodDelivery.Common.Models.Models.Food;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App;

public partial class FoodCard
{
	[Parameter, EditorRequired]
	public required FoodListModel Food { get; set; }
}
