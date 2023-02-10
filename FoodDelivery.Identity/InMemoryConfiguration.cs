using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FoodDelivery.Identity;

public static class InMemoryConfiguration
{

    public static readonly IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
    {
        new ApiScope("xyz")
    };


    public static IEnumerable<Client> Clients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "service.client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                Claims = new List<ClientClaim>()
                {
                    new ClientClaim(ClaimTypes.Name, "app1"),
                    new ClientClaim(ClaimTypes.NameIdentifier, "app1"),
                    new ClientClaim(ClaimTypes.Role, "client")
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "xyz" }
            }
        };
    }


}