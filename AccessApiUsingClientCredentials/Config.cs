using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AccessApiUsingClientCredentials
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
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // other clients omitted...

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                }
            };
        }
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
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Mohammad",
                    Password = "password"
                }
            };
        }
    }
}