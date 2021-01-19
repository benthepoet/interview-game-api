using System.Collections.Generic;

namespace GameAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public HashSet<int> GameIds { get; set; }
    }
}
