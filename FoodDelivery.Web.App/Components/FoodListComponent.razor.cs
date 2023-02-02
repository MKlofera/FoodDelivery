using FoodDelivery.Common;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App;

public partial class FoodListComponent
{
    class AllergenFilter
    {
        public Allergen Allergen { get; set; }
        public bool Display { get; set; }
    }

    [Parameter]
    public required ICollection<FoodListModel>? foods { get; set; }

    readonly Dictionary<Allergen, AllergenFilter> allergenFilters = Enum.GetValues<Allergen>()
        .Where(a => a != Allergen.Undefined)
        .Select(a => new AllergenFilter { Allergen = a, Display = true })
        .ToDictionary(a => a.Allergen, a => a);

    readonly Dictionary<string, Func<IEnumerable<FoodListModel>, IEnumerable<FoodListModel>>> orderings = new()
    {
        ["identityOrder"] = foods => foods,
        ["orderByNameAscending"] = foods => foods.OrderBy(f => f.Name),
        ["orderByNameDescending"] = foods => foods.OrderByDescending(f => f.Name),
        ["orderByPriceAscending"] = foods => foods.OrderBy(f => f.Price),
        ["orderByPriceDescending"] = foods => foods.OrderByDescending(f => f.Price),
    };
    string selectedOrderingKey = "identityOrder";

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    IEnumerable<FoodListModel> GetDisplayedFoods()
    {
        var ordering = orderings.GetValueOrDefault(selectedOrderingKey, foods => foods);
        return ordering(
            (foods ?? Enumerable.Empty<FoodListModel>())
            .Where(f => f.Allergens.All(a => allergenFilters[a].Display)));
    }
}
