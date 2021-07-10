using Microsoft.EntityFrameworkCore;
using MyWebApi.Context;
using MyWebApi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
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
                return await _context.accounts.ToListAsync();
            }
            else
            { 
                return await _context.accounts.Where(x => x.Delete == 0).ToListAsync();
            }
        }

        public async Task<AccountModel> GetPlayerByAccount(string account)
        {
            return await _context.accounts.Where(x => x.Account == account).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        // ------------ Interface ----------
    }
}
