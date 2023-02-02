namespace FoodDelivery.Common.Models.Models.FoodOrderNote;

public record FoodOrderNoteListModel : IWithId
{
    public Guid Id { get; init; }
    public string Note { get; set; }
    public Guid FoodId { get; set; }
    public string? FoodName { get; set; }
    public decimal FoodPrice { get; set; }
    public int FoodQuantity { get; set; }
    public Guid OrderId { get; set; }

    public FoodOrderNoteListModel(Guid id, string note, Guid foodId, string foodName, decimal foodPrice, int foodQuantity, Guid orderId)
    {
        Id = id;
        Note = note;
        FoodId = foodId;
        FoodName = foodName;
        FoodPrice = foodPrice;
        FoodQuantity = foodQuantity;
        OrderId = orderId;
    }
}