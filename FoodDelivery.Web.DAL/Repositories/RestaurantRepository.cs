using FoodDelivery.Common.Models;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.DAL;

namespace FoodDelivery.Web.DAL.Repositories
{
    public class RestaurantRepository : RepositoryBase<RestaurantDetailModel>
    {
        public override string TableName { get; } = "restaurants";

        public RestaurantRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}