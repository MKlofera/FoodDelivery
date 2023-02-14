using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.App.Controllers;

[ApiController]
[Authorize]
[Route("api/foods")]
public class FoodController : ControllerBase
{
    
    private readonly IFoodFacade _facade;

    public FoodController(IFoodFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FoodDetailModel> GetById(Guid id)
    {
        var food = _facade.GetById(id);
        return food != null ? Ok(food) : NotFound();
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Create(FoodDetailModel food)
    {
        var result = _facade.Create(food);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Guid> Update(FoodDetailModel food)
    {
        var result = _facade.Update(food);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public void Delete(Guid id)
    {
        _facade.Delete(id);
    }

}