using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Common.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.App.Controllers;


[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    
    private readonly IRestaurantFacade _restaurantFacade;
    private readonly IFoodFacade _foodFacade;


    public SearchController(IRestaurantFacade restaurantFacade, IFoodFacade foodFacade)
    {
        _restaurantFacade = restaurantFacade;
        _foodFacade = foodFacade;
    }


    [HttpGet("")]
    public ActionResult<SearchResultsModel> GetSearchResult([FromQuery(Name = "q")] string query)
    {
        return string.IsNullOrWhiteSpace(query)
            ? BadRequest()
            : Ok(new SearchResultsModel(
                _restaurantFacade.Search(query),
                _foodFacade.Search(query)));
    }
}