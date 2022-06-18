using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext()
        {

        }
        
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cacheId = "your-ncache-cache-id";
            var connectionString = "your-mysql-connection-string";

            NCacheConfiguration.Configure(cacheId, DependencyType.Other);
            NCacheConfiguration.ConfigureLogger();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        public DbSet<User> Users { get; set; }
    }
}
