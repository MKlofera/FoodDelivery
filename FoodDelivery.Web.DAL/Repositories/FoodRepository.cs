using FoodDelivery.Common.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Web.DAL;

namespace FoodDelivery.Web.DAL.Repositories
{
    public class FoodRepository : RepositoryBase<FoodDetailModel>
    {
        public override string TableName { get; } = "foods";

        public FoodRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}