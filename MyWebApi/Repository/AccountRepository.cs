using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using MyWebApi.Context;
using MyWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;
        private IServiceProvider _serviceProvider;

        public AccountRepository(AccountContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
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
                return await _context.accounts.AsNoTracking().ToListAsync();
            }
            else
            {
                using (var conn = new MySqlConnection("AccountContext"))
                {
                    var sql = "SELECT * FROM account WHERE delete = 0";
                    return await conn.QueryAsync<AccountModel>(sql);
                }
                //return await _context.accounts.AsNoTracking().Where(x => x.Delete == 0).ToListAsync();
            }
        }

        public async Task<AccountModel> GetPlayerByAccount(string account, bool isTracking = true)
        {
            //using (var conn = new MySqlConnection("AccountContext"))
            //{
            //    var sql = "SELECT * FROM account WHERE account = " + account;
            //    return await conn.QueryFirstAsync<AccountModel>(sql);
            //}

            if (isTracking)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AccountContext>();
                    try
                    {
                        return await db.accounts.Where(x => x.Account == account).SingleOrDefaultAsync();
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            else
            {
                using (var conn = new MySqlConnection("AccountContext"))
                {
                    var sql = "SELECT * FROM account WHERE account = @account";
                    return await conn.QueryFirstAsync<AccountModel>(sql, param: new { 
                        account
                    });
                }
                //return await _context.accounts.AsNoTracking().Where(x => x.Account == account).SingleOrDefaultAsync();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        // ------------ Interface ----------
    }
}
