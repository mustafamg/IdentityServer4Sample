﻿using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace IdentityServer4Sample.Server
{
    internal class Config
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // other clients omitted...

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
        //public static IEnumerable<Client> GetClients()
        //{
        //    return new List<Client>
        //    {
        //        // other clients omitted...

        //        // resource owner password grant client
        //        new Client
        //        {
        //            ClientId = "ro.client",
        //            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

        //            ClientSecrets =
        //            {
        //                new Secret("secret".Sha256())
        //            },
        //            AllowedScopes = { "api1" }
        //        }
        //    };
        //}
        //internal static IEnumerable<Client> GetClients()
        //{
        //    return new List<Client>
        //    {
        //        new Client
        //        {
        //            ClientId = "client",

        //            // no interactive user, use the clientid/secret for authentication
        //            AllowedGrantTypes = GrantTypes.ClientCredentials,

        //            // secret for authentication
        //            ClientSecrets =
        //            {
        //                new Secret("secret".Sha256())
        //            },

        //            // scopes that client has access to
        //            AllowedScopes = { "api1" }
        //        }
        //    };
        //}

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Mustafa",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim("name", "Mustafa"),
                        new Claim("website", "https://mustafamg.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Mohammad",
                    Password = "password",
                     Claims = new []
                    {
                        new Claim("name", "Mohammad"),
                        new Claim("website", "https://mustafamg.com")
                    }
                }
            };
        }
    }
}