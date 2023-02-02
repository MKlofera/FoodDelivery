using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL.Clients;

namespace FoodDelivery.Web.BL.Facades;
public class RestaurantFacade : FacadeBase<RestaurantDetailModel, RestaurantListModel>
{
    private readonly IRestaurantApiClient _apiClient;

    public RestaurantFacade(IRestaurantApiClient apiClient) : base()
    {
        this._apiClient = apiClient;
    }

    public override async Task<List<RestaurantListModel>> GetAllAsync()
    {
        // taking data from local storage and combine those with data from DB
        var restaurants = await base.GetAllAsync();
        var restaurantsFromApi = await _apiClient.RestaurantsGetAsync();
        foreach (var restaurantFromApi in restaurantsFromApi)
        {
            if (restaurants.Any(r => r.Id == restaurantFromApi.Id) is false)
            {
                restaurants.Add(restaurantFromApi);
            }
        }
        return restaurants;
    }

    public async Task<Guid> InsertRestaurantAsync(RestaurantDetailModel model)
    {
        return await _apiClient.RestaurantsPostAsync(model);
    }

    public override async Task<RestaurantDetailModel> GetByIdAsync(Guid id)
    {
        return await _apiClient.RestaurantsGetAsync(id.ToString());
    }

    protected override async Task<Guid> SaveToApiAsync(RestaurantDetailModel model)
    {
        return await _apiClient.RestaurantsPutAsync(model);
    }

    public async Task<Guid> SaveToApi(RestaurantDetailModel model)
    {
        return await _apiClient.RestaurantsPutAsync(model);
    }

    public async Task<List<FoodListModel>?> GetFoodsAsync(Guid id)
    {
        return await _apiClient.FoodsAsync(id.ToString()) as List<FoodListModel>;
    }

    public async Task<List<OrderListModel>?> GetOrdersAsync(Guid id)
    {
        return await _apiClient.OrdersAsync(id.ToString()) as List<OrderListModel>;
    }

    public async Task<double?> GetSalesAsync(Guid id)
    {
        return await _apiClient.SalesGetAsync(id.ToString());
    }

    public async Task<ICollection<RestaurantSalesModel>> GetAllSalesAsync()
    {
        return await _apiClient.SalesGetAsync();
    }

    public override async Task DeleteAsync(Guid id)
    {
        await _apiClient.RestaurantsDeleteAsync(id.ToString());
    }
}