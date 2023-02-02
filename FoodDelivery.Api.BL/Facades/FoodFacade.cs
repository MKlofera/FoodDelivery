using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Foods;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Food;

namespace FoodDelivery.Api.BL.Facades;

public class FoodFacade : IFoodFacade
{
    private readonly IFoodRepository repository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IMapper mapper;

    public FoodFacade(IFoodRepository repository, IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        this.repository = repository;
        this.restaurantRepository = restaurantRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Returns a list of all Foods
    /// </summary>
    /// <returns>An enumerable of all <see cref="FoodListModel"/></returns>
    public List<FoodListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<FoodListModel>>(entities);
    }

    /// <summary>
    /// Returns food with the given ID
    /// </summary>
    /// <param name="id">ID of the food to return.</param>
    /// <returns><see cref="FoodDetailModel"/> with the ID <paramref name="id"/>
    /// or null when the food does not exist.</returns>
    public FoodDetailModel? GetById(Guid id)
    {
        var foodEntity = repository.GetById(id);
        var restaurantEntity = restaurantRepository.GetById(foodEntity.RestaurantId);
        var foodModel = mapper.Map<FoodDetailModel>(foodEntity);
        foodModel.RestaurantName = restaurantEntity.Name;
        return mapper.Map<FoodDetailModel>(foodEntity);
    }

    /// <summary>
    /// Returns a list of all restaurant's foods
    /// </summary>
    /// <param name="id">ID of the restaurant to return it's foods.</param>
    /// <returns>An enumerable of all <see cref="FoodListModel"/></returns>
    public IList<FoodListModel> RestaurantFoods(Guid restaurantId)
    {
        var entities = repository.GetAll(f => f.RestaurantId == restaurantId);
        return mapper.Map<List<FoodListModel>>(entities);
    }

    /// <summary>
    /// Creates or updates (if it already exists) the food.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created/updated food</returns>
    public Guid CreateOrUpdate(FoodDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    /// <summary>
    /// Creates a food
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created food</returns>
    public Guid Create(FoodDetailModel model)
    {
        if (RestaurantFoodNameInDb(model))
            throw new RestaurantFoodWithSameNameException(model.Name);

        var entity = mapper.Map<FoodEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(FoodDetailModel model)
    {
        var entity = mapper.Map<FoodEntity>(model);
        return repository.Update(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }

    /// <summary>
    /// checks if certain name of restaurant name already exists in DB
    /// </summary>
    /// <param name="model"></param>
    /// <returns>true if restaurants name is already in DB</returns>
    public bool RestaurantFoodNameInDb(FoodDetailModel model)
    {
        return repository
            .GetAll(r => r.Name == model.Name && model.RestaurantId == model.RestaurantId)
            .Any();
    }

    public ICollection<FoodListModel> Search(string text)
    {
        var entities = repository.GetAll(f =>
            f.Name.Contains(text) || f.Description.Contains(text));
        return mapper.Map<List<FoodListModel>>(entities);
    }
}
