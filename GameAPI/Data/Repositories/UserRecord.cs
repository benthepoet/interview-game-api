using System.Collections.Generic;

namespace GameAPI.Data.Repositories
{
    public class UserRecord
    {
        public int Id { get; set; }
        public HashSet<int> GameIds { get; set; }
    }
}
