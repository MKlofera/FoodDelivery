﻿using Blazored.LocalStorage;

namespace FoodDelivery.Web.App.Handlers;


public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService localStorageService;

    public AuthenticationHeaderHandler(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await localStorageService.GetItemAsStringAsync("access-token");
        if (accessToken is not null)
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
