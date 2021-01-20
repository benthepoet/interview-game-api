using GameAPI.Models;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public class RAWGClient : IRAWGClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly IAsyncPolicy<GameList> _cachePolicy;

        public RAWGClient(IConfiguration configuration, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _apiKey = configuration.GetValue<string>("RAWG:ApiKey");
            _baseUrl = configuration.GetValue<string>("RAWG:BaseUrl");
            _httpClient = new HttpClient();
            _cachePolicy = policyRegistry.Get<IAsyncPolicy<GameList>>("myCachePolicy");
        }

        private string BuildUri(string resource, string query = "")
        {
            var uri = new UriBuilder($"{_baseUrl}/{resource}?key={_apiKey}&{query}");
            return uri.ToString();
        }

        public async Task<Game> GetGame(int gameId)
        {
            var uri = BuildUri($"games/{gameId}");
            return await _httpClient.GetFromJsonAsync<Game>(uri);
        }

        public async Task<GameList> ListGames(string search, string sort)
        {
            var uri = BuildUri("games", $"search={search}");

            return await _cachePolicy.ExecuteAsync(async (context) =>
            {
                return await _httpClient.GetFromJsonAsync<GameList>(uri);
            }, new Context(uri));
            
        }
    }
}
