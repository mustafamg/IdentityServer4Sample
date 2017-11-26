using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
namespace IdentityServer4Sample.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start execution when the servers are ready.");
            Console.ReadLine();
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;
                        // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("test@test.com", "Admin@123", "api1").Result;

            //var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("http://localhost:5001/api/identity").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadLine();
        }
    }
}