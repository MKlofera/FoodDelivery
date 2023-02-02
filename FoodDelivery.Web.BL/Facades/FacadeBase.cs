using System.Globalization;
using FoodDelivery.Common.BL;

namespace FoodDelivery.Web.BL.Facades;
public abstract class FacadeBase<TDetailModel, TListModel> : IAppFacade
    where TDetailModel : class
{
    // private readonly RepositoryBase<TDetailModel> repository;
    // private readonly IMapper mapper;
    // private readonly LocalDbOptions localDbOptions;
    protected virtual string culture => CultureInfo.DefaultThreadCurrentCulture?.Name ?? "cs";

    // protected FacadeBase(
        // RepositoryBase<TDetailModel> repository,
        // IMapper mapper
        // IOptions<LocalDbOptions> localDbOptions
        // )
    // {
        // this.repository = repository;
        // this.mapper = mapper;
        // this.localDbOptions = localDbOptions.Value;
    // }

    public virtual Task<List<TListModel>> GetAllAsync()
    {
        //This method is for taking data from local storage 
        var itemsAll = new List<TListModel>();
        return Task.FromResult(itemsAll);
    }

    public abstract Task<TDetailModel> GetByIdAsync(Guid id);

    public virtual async Task SaveAsync(TDetailModel data)
    {
        try
        {
            await SaveToApiAsync(data);
        }
        catch (HttpRequestException exception) when (exception.Message.Contains("Failed to fetch"))
        {

        }
    }

    protected abstract Task<Guid> SaveToApiAsync(TDetailModel data);
    public abstract Task DeleteAsync(Guid id);
}
