using Dapper;
using Dapper.FluentMap;
using MySql.Data.MySqlClient;
using MyWebApi.Model;
using MyWebApi.Repository.Map;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private string _connectString = "Data Source=localhost;port=3306;User ID=root; Password=123456; database='account';";
        private string _tableName = "accounts";

        public AccountRepository()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AccountModelMap());
            });
        }

        // ------------ Interface ----------
        public async Task Add(AccountModel model)
        {
            using var connection = new MySqlConnection(_connectString);
            var strCmd = "INSERT INTO " + _tableName + " (account, password, name, inactive) VALUES(@account, @password, @name, @inactive);";
            await connection.ExecuteAsync(strCmd, model);
        }

        public async Task UpdateInActive(AccountModel model)
        {
            using var connection = new MySqlConnection(_connectString);
            var strCmd = "UPDATE " + _tableName + " SET inactive = @inactive WHERE account = @account";
            await connection.ExecuteAsync(strCmd, model);
        }

        public async Task Delete(AccountModel model)
        {
            using var connection = new MySqlConnection(_connectString);
            var strCmd = "DELETE FROM " + _tableName + " WHERE account = @account";
            await connection.ExecuteAsync(strCmd, model);
        }

        public async Task DeleteByAccount(string account)
        {
            var player = await GetPlayerByAccount(account);
            if (player != null)
                await Delete(player);
        }

        public async Task<IEnumerable<dynamic>> GetPlayerList(bool containDelete)
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

        public async Task<dynamic> GetPlayerByAccount(string account, bool isTracking = true)
        {
            // 記憶體提升最少
            using (var conn = new MySqlConnection(_connectString))
            {
                var sql = "SELECT * FROM accounts WHERE account = @account";
                return await conn.QueryFirstOrDefaultAsync<AccountModel>(sql, param: new
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

        // ------------ Interface ----------
    }
}
