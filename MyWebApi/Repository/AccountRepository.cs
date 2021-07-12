using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using MyWebApi.Context;
using MyWebApi.Model;
using MyWebApi.Repository.Map;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;

        private string _connectString = "Data Source=localhost;port=3306;User ID=root; Password=123456; database='account';";

        public AccountRepository(AccountContext context)
        {
            _context = context;

            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AccountModelMap());
            });
        }

        // ------------ Interface ----------
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task DeleteByAccount(string account)
        {
            var player = await GetPlayerByAccount(account);
            if (player != null)
                Delete(player);
        }

        public async Task<IEnumerable<AccountModel>> GetPlayerList(bool containDelete)
        {
            if (containDelete)
            {
                using (var conn = new MySqlConnection(_connectString))
                {
                    var sql = "SELECT * FROM accounts";
                    return await conn.QueryAsync<AccountModel>(sql);
                }
            }
            else
            {
                using (var conn = new MySqlConnection(_connectString))
                {
                    var sql = "SELECT * FROM accounts WHERE inactive = 0";
                    return await conn.QueryAsync<AccountModel>(sql);
                }
            }
        }

        public async Task<AccountModel> GetPlayerByAccount(string account, bool isTracking = true)
        {
            // 記憶體提升最少
            using (var conn = new MySqlConnection(_connectString))
            {
                var sql = "SELECT * FROM accounts WHERE account = @account";
                return await conn.QueryFirstAsync<AccountModel>(sql, param: new
                {
                    account
                });
            }

            // 下兩方法 記憶體提升差不多
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AccountContext>();
            //    try
            //    {
            //        return await db.accounts.AsNoTracking().Where(x => x.Account == account).SingleOrDefaultAsync();
            //    }
            //    finally
            //    {
            //        db.Dispose();
            //    }
            //}

            //return await _context.accounts.AsNoTracking().Where(x => x.Account == account).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        // ------------ Interface ----------
    }
}
