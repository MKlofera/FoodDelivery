using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FoodDelivery.Web.App;

public partial class AddToCartButton
{
    [Parameter, EditorRequired]
    public required FoodListModel Food { get; set; }

    private ShoppingCartItemModel CartItem { get; set; } = new();

    /**
     * is called after user clicks on food, set's some info about food and restaurant
     */
    private void SetInfoAboutOrderingFood(FoodListModel food)
    {
        CartItem.FoodName = food.Name;
        CartItem.RestaurantId = food.RestaurantId;
        CartItem.FoodId = food.Id;
        CartItem.FoodPrice = food.Price;
        AddToCartHandler();
    }

    /**
     * Is called after user click's save food to cart.
     * Check's quantity, and adds food to cart
     */
    private async void AddToCartHandler()
    {
        if (CartItem.Quantity > 0)
        {
            IList<ShoppingCartItemModel> orders = new List<ShoppingCartItemModel>();

            orders = await GetCartItemsFromLocalStorage(orders);

            if (OrderFromSingleRestaurant(orders))
            {
                AddItemsToCart(orders);
            }
            else
            {
                await JsRuntime.InvokeVoidAsync("alert", "Warning order from multiple restaurants!");
            }
        }
        else
        {
            await JsRuntime.InvokeVoidAsync("alert", "Warning order has no possitive quantity number!");
        }
    }

    /**
     * @brief validate if ordering food is from the same restaurant like already ordered food
     */
    private bool OrderFromSingleRestaurant(IList<ShoppingCartItemModel> orders)
    {
        if (orders.Count > 0)
        {
            foreach (var order in orders)
            {
                if (order.RestaurantId != CartItem.RestaurantId)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /**
     * @brief If food with same description/note is already in cart, increase quantity
     * if not, add to cart
     */
    private async void AddItemsToCart(IList<ShoppingCartItemModel> orders)
    {
        bool OrderingFoodIsAlreadyInCart = false;
        foreach (var order in orders)
        {
            if (CartItem.FoodId == order.FoodId && CartItem.Note == order.Note)
            {
                order.Quantity += CartItem.Quantity;
                OrderingFoodIsAlreadyInCart = true;
            }
        }

        if (!OrderingFoodIsAlreadyInCart)
        {
            orders.Add(CartItem);
        }
        await localStorage.SetItemAsync("ShoppingCart", orders);

    }

    /**
     * @brief Get's all items from local storage
     */
    private async Task<IList<ShoppingCartItemModel>> GetCartItemsFromLocalStorage(IList<ShoppingCartItemModel> orders)
    {
        bool localStorageOfShoppingCartExists = await localStorage.ContainKeyAsync("ShoppingCart");
        if (localStorageOfShoppingCartExists)
        {
            orders = await localStorage.GetItemAsync<IList<ShoppingCartItemModel>>("ShoppingCart");
        }

        return orders;
    }
}