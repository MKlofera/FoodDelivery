using FoodDelivery.Common.Models.Models.Restaurant;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App;

public partial class RestaurantCard
{
    [Parameter, EditorRequired]
    public required RestaurantListModel Restaurant { get; set; }
}
