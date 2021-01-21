using GameAPI.Exceptions;
using GameAPI.Models;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Caching;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public class RAWGClient : IRAWGClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly IAsyncPolicy<GameResponse> _gamePolicy;
        private readonly IAsyncPolicy<GamesResponse> _gamesPolicy;
        private readonly HttpClient _httpClient;

        private const int DEFAULT_GAME_CACHE_TTL = 10;
        private const string CONFIG_API_KEY = "RAWG:ApiKey";
        private const string CONFIG_BASE_URL = "RAWG:BaseUrl";
        private const string CONFIG_GAME_CACHE_TTL = "RAWG:GameCacheTTL";

        private static readonly string[] SORT_FIELDS = { 
            "name",
            "released",
            "added",
            "created",
            "updated",
            "rating",
            "metacritic"
        };

        public RAWGClient(IConfiguration configuration, IAsyncCacheProvider cacheProvider, HttpClient httpClient)
        {
            _apiKey = configuration.GetValue<string>(CONFIG_API_KEY);

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new ArgumentNullException($"The configuration value '{CONFIG_API_KEY}' is not set.");
            }

            _baseUrl = configuration.GetValue<string>(CONFIG_BASE_URL);

            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new ArgumentNullException($"The configuration value '{CONFIG_BASE_URL}' is not set.");
            }

            _httpClient = httpClient;

            // Build a basic retry policy
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(new [] { 
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                });

            var gameCacheTTL = configuration.GetValue<int?>(CONFIG_GAME_CACHE_TTL) ?? DEFAULT_GAME_CACHE_TTL;

            // Game requests have caching and retry for fault tolerance
            _gamePolicy = Policy
                .WrapAsync(
                    Policy.CacheAsync(
                        cacheProvider, 
                        TimeSpan.FromMinutes(gameCacheTTL)), 
                    retryPolicy)
                .AsAsyncPolicy<GameResponse>();

            // Games requests have just retry for fault tolerance
            _gamesPolicy = retryPolicy.AsAsyncPolicy<GamesResponse>();
        }

        public async Task<GameResponse> GetGame(int gameId)
        {
            var uri = BuildUri($"games/{gameId}");

            return await _gamePolicy.ExecuteAsync(
                async (context) => {
                    var response = await _httpClient.GetAsync(uri);
                    
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }

                    return await response.Content.ReadFromJsonAsync<GameResponse>();
                }, 
                new Context(uri));
        }

        public async Task<GamesResponse> ListGames(string search, string sort = "")
        {
            var uri = BuildUri("games", $"search={search}");

            if (!string.IsNullOrEmpty(sort))
            {
                if (!IsSortValid(sort))
                {
                    throw new ArgumentException($"'{sort}' is not a valid sort parameter");
                }

                uri += $"&ordering={sort}";
            }

            return await _gamesPolicy.ExecuteAsync(
                async () => await _httpClient.GetFromJsonAsync<GamesResponse>(uri));
        }

        private string BuildUri(string resource, string query = "")
        {
            var uri = new UriBuilder($"{_baseUrl}/api/{resource}?key={_apiKey}&{query}");
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
