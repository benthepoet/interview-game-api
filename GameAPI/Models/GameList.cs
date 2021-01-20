using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Models
{
    public class GameList
    {
        public int Count { get; set; }
        public string Next { get; set; }

        public IEnumerable<Game> Results { get; set; }
    }
}
