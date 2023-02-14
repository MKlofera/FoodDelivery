using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FoodDelivery.Api.App.Controllers;

[ApiController]
[Authorize]
[Route("api/restaurants")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IFoodFacade _foodFacade;
    private readonly IOrderFacade _orderFacade;

    public RestaurantController(IRestaurantFacade restaurantFacade, IFoodFacade foodFacade, IOrderFacade orderFacade)
    {
        _restaurantFacade = restaurantFacade;
        _foodFacade = foodFacade;
        _orderFacade = orderFacade;
    }
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ICollection<RestaurantListModel>> GetAll()
    {
        var restaurants = _restaurantFacade.GetAll();
        return restaurants != null ? Ok(restaurants) : NotFound();
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantDetailModel> GetById(Guid id)
    {
        var restaurant = _restaurantFacade.GetById(id);
        return restaurant != null ? Ok(restaurant) : NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Create(RestaurantDetailModel restaurant)
    {
        var result = _restaurantFacade.Create(restaurant);
        return  result != null ? Ok(result) : NotFound();
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Update(RestaurantDetailModel restaurant)
    {
        var result = _restaurantFacade.Update(restaurant);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public void Delete(Guid id)
    {
        _restaurantFacade.Delete(id);
    }

    /// <summary>
    /// Return's a restaurant sales
    /// </summary>
    /// <param name="id"></param>
    /// <returns>decimal number of restaurants sales</returns>
    [HttpGet("{restaurantId:guid}/sales")]
    public decimal GetRestaurantSales(Guid id)
    {
        var restaurantSales = _restaurantFacade.GetSales(id, DateTime.MinValue, DateTime.MaxValue);
        return restaurantSales != null ? restaurantSales.Value : 0;
    }

    [HttpGet("sales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RestaurantSalesModel> GetAllRestaurantSales(Guid id)
    {
        var allSales = _restaurantFacade
            .GetAll()
            .Select(r => new RestaurantSalesModel(Restaurant: r, GetRestaurantSales(r.Id)));
            
        return allSales != null ? Ok(allSales) : NotFound();
    }

    [HttpGet("{restaurantId:guid}/foods")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ICollection<FoodListModel>> GetRestaurantsFoods(Guid restaurantId)
    {
        var foods = _foodFacade.RestaurantFoods(restaurantId);
        return foods != null ? Ok(foods) : NotFound();
    }

    [HttpGet("{restaurantId:guid}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ICollection<OrderListModel>> GetRestaurantOrders(Guid restaurantId)
    {
        var orders = _orderFacade.RestaurantOrders(restaurantId);
        return orders != null ? Ok(orders) : NotFound();
    }
}