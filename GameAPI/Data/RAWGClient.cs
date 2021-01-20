using GameAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameAPI.Data
{
    public class RAWGClient : IRAWGClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public RAWGClient(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("RAWG.ApiKey");
            _baseUrl = configuration.GetValue<string>("RAWG.BaseUrl");
        }

        public Game GetGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> ListGames(string search, string sort)
        {
            var client = new HttpClient();
            return null;
        }
    }
}
