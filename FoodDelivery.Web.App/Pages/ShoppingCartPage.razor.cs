using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;
using System.Numerics;

namespace FoodDelivery.Web.App.Pages;

public partial class ShoppingCartPage
{
    List<ShoppingCartItemModel> CartItems = new List<ShoppingCartItemModel>();

    [Inject] 
    private OrderFacade OrderFacade { get; set; } = null!;

    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    private OrderDetailModel NewOrder = new OrderDetailModel();

    protected override async Task OnInitializedAsync()
    {
        
        CartItems = await localStorage.GetItemAsync<List<ShoppingCartItemModel>>("ShoppingCart");
        await base.OnInitializedAsync();
    }

    /**
     * This method is called when the user clicks on the "Order" button.
     * It creates a new order and redirects the user to the "OrderDetailPage".
     */
    private async void DeleteItem(ShoppingCartItemModel selectedItem)
    {
        if (selectedItem.Quantity > 1)
        {
            foreach (var item in CartItems)
            {
                if (item.FoodId == selectedItem.FoodId && item.Note == selectedItem.Note)
                {
                    item.Quantity--;
                }
            }
        }
        else
        {
            CartItems.RemoveAll(x => x.FoodId == selectedItem.FoodId && x.Note == selectedItem.Note);
        }
        await localStorage.SetItemAsync("ShoppingCart", CartItems);
    }

    /**
     * This method is called when the user clicks on the "Delete whole cart" button.
     * It creates a new order and redirects the user to the "OrderDetail" page.
     */
    private async void DeleteWholeCart()
    {
        if (CartItems is not null)
        {
            CartItems.Clear();
            await localStorage.SetItemAsync("ShoppingCart", CartItems);
        }
    }


    /**
     * @brief Creates new order with selected foods and sends it to server
     */
    private async void CreateOrder()
    {
        if (CartItems is null) return;

        NewOrder.CreatedDate = DateTime.Now;
        NewOrder.OrderState = OrderState.Ordered;
        NewOrder.RestaurantId = CartItems[0].RestaurantId;

        foreach (var order in CartItems)
        {
            FoodOrderNoteListModel foodOrderNote = new(Guid.NewGuid(), order.Note, order.FoodId, order.FoodName, order.FoodPrice, order.Quantity, NewOrder.Id);
            NewOrder.FoodOrderNotes.Add(foodOrderNote);
        }
        await OrderFacade.InsertOrdersAsync(NewOrder);

        DeleteWholeCart();

        navigationManager.NavigateTo($"/restaurants");

    }
}
