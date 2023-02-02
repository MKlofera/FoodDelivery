using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Web.BL.Clients;

namespace FoodDelivery.Web.BL.Facades;

public class OrderFacade : IAppFacade
{
    private readonly IOrderApiClient _apiClient;


    public OrderFacade(IOrderApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<ICollection<OrderListModel>> GetAllAsync()
    {
        return await _apiClient.OrdersGetAsync();
    }

    public async Task<OrderDetailModel> GetByIdAsync(Guid id)
    {
        return await _apiClient.OrdersGetAsync(id.ToString());
    }

    public async Task<Guid> InsertOrdersAsync(OrderDetailModel model)
    {
        return await _apiClient.OrdersPostAsync(model);
    }

    public async Task<Guid> SaveToApi(OrderDetailModel model)
    {
        return await _apiClient.OrdersPutAsync(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _apiClient.OrdersDeleteAsync(id.ToString());
    }
}