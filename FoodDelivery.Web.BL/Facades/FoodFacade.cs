using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Web.BL.Clients;

namespace FoodDelivery.Web.BL.Facades;

public class FoodFacade : IAppFacade
{
    private readonly IFoodApiClient _apiClient;

    public FoodFacade(IFoodApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<FoodDetailModel> GetByIdAsync(Guid id)
    {
        return await _apiClient.FoodsGetAsync(id.ToString());
    }

    public async Task<Guid> InserFoodAsync(FoodDetailModel model)
    {
        return await _apiClient.FoodsPostAsync(model);
    }

    public async Task<Guid> SaveToApiAsync(FoodDetailModel model)
    {
        return await _apiClient.FoodsPutAsync(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _apiClient.FoodsDeleteAsync(id.ToString());
    }
}