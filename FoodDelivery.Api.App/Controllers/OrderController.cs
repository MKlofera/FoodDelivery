using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.App.Controllers;

[ApiController]
[Authorize]
[Route("api/orders/")]
public class OrderController : ControllerBase
{
    private readonly IOrderFacade _facade;

    public OrderController(IOrderFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ICollection<OrderListModel>> GetAll()
    {
        var orders = _facade.GetAll();
        return orders != null ? Ok(orders) : NotFound();
    }



    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderDetailModel> GetById(Guid id)
    {
        var order = _facade.GetById(id);
        return order != null ? Ok(order) : NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Create(OrderDetailModel order)
    {
        var result = _facade.Create(order);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Update(OrderDetailModel order)
    {
        var result = _facade.Update(order);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public void Delete(Guid id)
    {
        _facade.Delete(id);
    }
}