using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.Order;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App;

public partial class OrderCard
{
	[Parameter, EditorRequired]
	public required OrderListModel Order { get; set; }
}
