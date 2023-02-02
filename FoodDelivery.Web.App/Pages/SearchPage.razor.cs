using FoodDelivery.Common;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Web.BL;
using FoodDelivery.Web.BL.Facades;

using Microsoft.AspNetCore.Components;

namespace FoodDelivery.Web.App.Pages;

public partial class SearchPage
{
    [Inject]
    private SearchFacade SearchFacade { get; set; } = null!;

    SearchResultsModel? searchResults;
    Task<SearchResultsModel>? searchTask;
    string? searchQuery;

    bool IsLoading => searchTask is { IsCompleted: false };

    public async Task StartSearch(string? query)
    {
        searchQuery = query;

        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            searchResults = null;
            return;
        }

        searchTask = SearchFacade.SearchAsync(searchQuery);
        searchResults = await searchTask;
    }
}
