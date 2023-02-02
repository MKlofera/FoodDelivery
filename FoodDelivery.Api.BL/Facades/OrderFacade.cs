using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Order;

namespace FoodDelivery.Api.BL.Facades;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderRepository repository;
    private readonly IRestaurantRepository RestaurantRepository;
    private readonly IMapper mapper;

    public OrderFacade(IOrderRepository repository, IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        this.RestaurantRepository = restaurantRepository;
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// returns a list of all orders
    /// </summary>
    /// <returns>An enumerable of all <see cref="OrderListModel"/></returns>
    public List<OrderListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<OrderListModel>>(entities);
    }

    /// <summary>
    /// Returns order with the given ID
    /// </summary>
    /// <param name="id">ID of the order to return.</param>
    /// <returns><see cref="OrderDetailModel"/> with the ID <paramref name="id"/>
    /// or null when the order does not exist.</returns>
    public OrderDetailModel? GetById(Guid id)
    {
        var orderEntity = repository.GetById(id);
        var restaurant = RestaurantRepository.GetById(orderEntity.RestaurantId);
        if (orderEntity == null)
        {
            throw new EntityNotFoundException(id, "OrderEntity");
        }
        var OrderModel = mapper.Map<OrderDetailModel>(orderEntity);
        OrderModel.RestaurantName= restaurant.Name;
        return OrderModel;
    }

    /// <summary>
    /// Creates or updates (if it already exists) the order.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created/updated order</returns>
    public Guid CreateOrUpdate(OrderDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    /// <summary>
    /// Creates a order
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created order</returns>
    public Guid Create(OrderDetailModel model)
    {
        var entity = mapper.Map<OrderEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(OrderDetailModel model)
    {
        var entity = mapper.Map<OrderEntity>(model);
        return repository.Update(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }

    /// <summary>
    /// Get orders for specific restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns>list of <see cref="OrderListModel"/></returns>
    public List<OrderListModel> RestaurantOrders(Guid restaurantId)
    {
        var restaurantOrders = new List<OrderListModel>();
        var allOrders = GetAll();

        foreach (var order in allOrders)
        {
            if(order.RestaurantId == restaurantId)
            {
                restaurantOrders.Add(order);
            }
        }
        return restaurantOrders;
    }
}