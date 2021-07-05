using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Model.Context
{
    public class LoginContext: DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }

        public virtual DbSet<AccountModel> accountitems { get; set; }
    }
}
