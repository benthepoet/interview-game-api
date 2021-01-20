using GameAPI.Models;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Registry;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using GameAPI.Exceptions;

namespace GameAPI.Data
{
    public class RAWGClient : IRAWGClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly IAsyncPolicy<Game> _cachePolicy;
        private readonly HttpClient _httpClient;

        private static readonly string[] SORT_FIELDS = { 
            "name",
            "released",
            "added",
            "created",
            "updated",
            "rating",
            "metacritic"
        };

        public RAWGClient(IConfiguration configuration, IReadOnlyPolicyRegistry<string> policyRegistry, HttpClient httpClient)
        {
            _apiKey = configuration.GetValue<string>("RAWG:ApiKey");
            _baseUrl = configuration.GetValue<string>("RAWG:BaseUrl");
            _cachePolicy = policyRegistry.Get<IAsyncPolicy<Game>>("gameCachePolicy");
            _httpClient = httpClient;
        }

        public async Task<Game> GetGame(int gameId)
        {
            var uri = BuildUri($"games/{gameId}");

            return await _cachePolicy.ExecuteAsync(async (context) =>
            {
                return await _httpClient.GetFromJsonAsync<Game>(uri);
            }, new Context(uri));
        }

        public async Task<GameList> ListGames(string search, string sort = "")
        {
            var uri = BuildUri("games", $"search={search}");

            if (!string.IsNullOrEmpty(sort))
            {
                if (!IsSortValid(sort))
                {
                    throw new InvalidParameterException($"'{sort}' is not a valid sort parameter");
                }

                uri += $"&ordering={sort}";
            }

            return await _httpClient.GetFromJsonAsync<GameList>(uri);
        }

        private string BuildUri(string resource, string query = "")
        {
            var uri = new UriBuilder($"{_baseUrl}/{resource}?key={_apiKey}&{query}");
            return uri.ToString();
        }

        public static bool IsSortValid(string sort) 
        {
            var pattern = @"^[-]*(?<field>\w+)$";
            var regex = new Regex(pattern);

            var match = regex.Match(sort);
            if (match == null)
            {
                return false;
            }

            var field = match.Groups["field"];

            if (!SORT_FIELDS.Contains(field.Value))
            {
                return false;
            }

            return true;
        }
    }
}
