using Microsoft.EntityFrameworkCore;
using MyWebApi.Model;

namespace MyWebApi.Context
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public virtual DbSet<AccountModel> accounts { get; set; }
    }
}
