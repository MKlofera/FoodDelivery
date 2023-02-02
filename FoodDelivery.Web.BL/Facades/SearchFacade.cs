using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Web.BL.Clients;

namespace FoodDelivery.Web.BL.Facades;

public class SearchFacade : IAppFacade
{
    private readonly ISearchApiClient _apiClient;

    public SearchFacade(ISearchApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<SearchResultsModel> SearchAsync(string query)
    {
        return await _apiClient.SearchAsync(query);
    }
}