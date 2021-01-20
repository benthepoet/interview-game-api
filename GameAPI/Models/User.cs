using System.Collections.Generic;

namespace GameAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public HashSet<int> GameIds { get; set; }

        public User Clone()
        {
            return new User
            {
                Id = Id,
                GameIds = new HashSet<int>(GameIds)
            };
        }
    }
}
