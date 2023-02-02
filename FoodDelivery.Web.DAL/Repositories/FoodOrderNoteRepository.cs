using FoodDelivery.Common.Models;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Web.DAL;

namespace FoodDelivery.Web.DAL.Repositories
{
    public class FoodOrderNoteRepository : RepositoryBase<FoodOrderNoteDetailModel>
    {
        public override string TableName { get; } = "foodOrderNotes";

        public FoodOrderNoteRepository(LocalDb localDb)
            : base(localDb)
        {
        }
    }
}