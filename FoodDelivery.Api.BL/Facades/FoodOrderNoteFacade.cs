using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Api.BL.Facades;

public class FoodOrderNoteFacade : IFoodOrderNoteFacade
{
    private readonly IFoodOrderNoteRepository repository;
    private readonly IMapper mapper;

    public FoodOrderNoteFacade(IFoodOrderNoteRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// returns a list of all food order notes
    /// </summary>
    /// <returns>An enumerable of all <see cref="FoodOrderNoteListModel"/></returns>
    public List<FoodOrderNoteListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<FoodOrderNoteListModel>>(entities);
    }

    /// <summary>
    /// Returns food order note with the given ID
    /// </summary>
    /// <param name="id">ID of the food order note to return.</param>
    /// <returns><see cref="FoodOrderNoteDetailModel"/> with the ID <paramref name="id"/>
    /// or null when the food order note does not exist.</returns>
    public FoodOrderNoteDetailModel? GetById(Guid id)
    {
        var entity = repository.GetById(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(id, "FoodOrderNoteEntity");
        }
        return mapper.Map<FoodOrderNoteDetailModel>(entity);
    }

    /// <summary>
    /// Creates or updates (if it already exists) the food order note.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created/updated food order note</returns>
    public Guid CreateOrUpdate(FoodOrderNoteDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }
    /// <summary>
    /// Creates a food order note
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created food order note</returns>
    public Guid Create(FoodOrderNoteDetailModel model)
    {
        var entity = mapper.Map<FoodOrderNoteEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(FoodOrderNoteDetailModel model)
    {
        var entity = mapper.Map<FoodOrderNoteEntity>(model);
        return repository.Insert(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }
}