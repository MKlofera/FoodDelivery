using FoodDelivery.Common.Models;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Web.DAL;

namespace FoodDelivery.Web.DAL.Repositories
{
    public class OrderRepository : RepositoryBase<OrderDetailModel>
    {
        public override string TableName { get; } = "orders";

        public OrderRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}