using GameAPI.Models;
using System.Collections.Generic;

namespace GameAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private int _nextKey;
        private readonly IDictionary<int, User> _storage;

        public UserRepository()
        {
            _nextKey = 0;
            _storage = new Dictionary<int, User>();
        }

        public User CreateUser()
        {
            var user = new User { 
                Id = _nextKey, 
                GameIds = new HashSet<int>()
            };

            _storage[user.Id] = user;
            _nextKey++;

            return user;
        }

        public User GetUser(int id)
        {
            if (_storage.ContainsKey(id))
            {
                var user = _storage[id];
                
                // Return a deep clone so we're not passing a reference to the stored object
                return user.Clone();
            }

            return null;
        }

        public void UpdateUser(User user)
        {
            if (_storage.ContainsKey(user.Id))
            {
                _storage[user.Id] = user;
            }
        }
    }
}
