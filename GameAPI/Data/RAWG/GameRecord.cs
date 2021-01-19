﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Data.RAWG
{
    public class GameRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Added { get; set; }
        public int Metacritic { get; set; }
        public decimal Rating { get; set; }
        public string Released { get; set; }
        public string Updated { get; set; }
    }
}
