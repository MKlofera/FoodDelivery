using FoodDelivery.Common.Enums;

namespace FoodDelivery.Common.Models.Models.Food;

public record FoodListModel : IWithId
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string? Photo { get; set; }
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
    public ICollection<Allergen> Allergens { get; set; } = new List<Allergen>();

    public FoodListModel(Guid id, string name, string? photo, decimal price, Guid restaurantId)
    {
        Id = id;
        Name = name;
        Photo = photo;
        Price = price;
        RestaurantId = restaurantId;
    }
}