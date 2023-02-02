using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Common.Models.Models.Restaurant;

public record RestaurantSalesModel(
    RestaurantListModel Restaurant,
    decimal Sales);
