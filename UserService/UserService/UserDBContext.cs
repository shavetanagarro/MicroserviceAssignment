using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService
{
    public class UserDBContext: DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }


    }
}
