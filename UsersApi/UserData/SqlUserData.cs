using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.UserData
{
    public class SqlUserData : IUserData
    {
        private UserContext _context;
        public SqlUserData(UserContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> GetUsersFromCacheAsCollection()
        {
            var options = new CachingOptions
            {
                StoreAs = StoreAs.Collection
            };

            var users = (from user in _context.Users
                         select user).FromCache(options).ToList();

            return users;
        }

        public List<User> GetUsersFromCacheAsSeperateEntities()
        {
            var options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };

            var users = (from user in _context.Users
                         select user).FromCache(options).ToList();

            return users;
        }

        public List<User> GetUsersFromCacheOnly()
        {
            var users = (from user in _context.Users
                         where user.Id > 1
                         select user).FromCacheOnly().ToList();

            return users;
        }

        public List<User> LoadDataIntoCache()
        {
            var options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };

            var users = (_context.Users).LoadIntoCache(options).ToList();

            return users;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
