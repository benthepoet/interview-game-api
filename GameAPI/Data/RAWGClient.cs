using GameAPI.Exceptions;
using GameAPI.Models;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _apiKey = configuration.GetValue<string>("RAWG:ApiKey");
            _baseUrl = configuration.GetValue<string>("RAWG:BaseUrl");
            _httpClient = httpClient;

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(new [] { 
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                });

            // Game requests have caching and retry for fault tolerance
            _gamePolicy = Policy
                .WrapAsync(
                    Policy.CacheAsync(
                        cacheProvider, 
                        TimeSpan.FromMinutes(configuration.GetValue<int>("RAWG:GameCacheTTL"))), 
                    retryPolicy)
                .AsAsyncPolicy<GameResponse>();

            // Games requests have just retry for fault tolerance
            _gamesPolicy = retryPolicy.AsAsyncPolicy<GamesResponse>();
        }

        public async Task<GameResponse> GetGame(int gameId)
        {
            var uri = BuildUri($"games/{gameId}");

            return await _gamePolicy.ExecuteAsync(
                async (context) => await _httpClient.GetFromJsonAsync<GameResponse>(uri), 
                new Context(uri));
        }

        public async Task<GamesResponse> ListGames(string search, string sort = "")
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

            return await _gamesPolicy.ExecuteAsync(
                async () => await _httpClient.GetFromJsonAsync<GamesResponse>(uri));
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
