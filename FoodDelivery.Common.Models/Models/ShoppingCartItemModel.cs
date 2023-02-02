using Guid = System.Guid;

namespace FoodDelivery.Common.Models.Models;

public record ShoppingCartItemModel
{
    public string Note { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid FoodId { get; set; }
    public string FoodName { get; set; }
    public int Quantity { get; set; }
    public decimal FoodPrice { get; set; }

    public ShoppingCartItemModel(string note, Guid restaurantId, Guid foodId, string foodName, int quantity, decimal foodPrice)
    {
        Note = note;
        RestaurantId = restaurantId;
        FoodId = foodId;
        FoodName = foodName;
        Quantity = quantity;
        FoodPrice = foodPrice;
    }

    public ShoppingCartItemModel()
    {
        Note = string.Empty;
        RestaurantId = Guid.Empty;
        FoodId = Guid.Empty;
        FoodName = string.Empty;
        Quantity = 0;
        FoodPrice = 0;
    }
}