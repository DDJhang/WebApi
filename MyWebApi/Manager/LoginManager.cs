using MyWebApi.Model;
using System.Collections.Concurrent;

namespace MyWebApi.Manager
{
    public class LoginManager
    {
        private ConcurrentDictionary<string, AccountModelDTO> _accountDict = new ConcurrentDictionary<string, AccountModelDTO>();

        public LoginManager() { }

        public bool Add(AccountModelDTO model)
        {
            var account = model.Account;
            return _accountDict.TryAdd(account, model);
        }

        public AccountModelDTO Get(string account)
        {
            if (_accountDict.TryGetValue(account, out AccountModelDTO outModel))
            {
                return outModel;
            }
            return null;
        }

        public AccountModelDTO[] GetArray()
        {
            if (_accountDict.Count == 0)
                return null;

            var toArray = _accountDict.ToArray();

            AccountModelDTO[] array = new AccountModelDTO[toArray.Length];
            for (int i = 0; i < array.Length; i++)
                array[i] = toArray[i].Value;

            return array;
        }

        public string GetName(string account)
        {
            if (_accountDict.TryGetValue(account, out AccountModelDTO outModel))
            {
                return outModel.Name;
            }
            return string.Empty;
        }

        public bool Remove(AccountModel model)
        {
            return _accountDict.TryRemove(model.Account, out AccountModelDTO outModel);
        }

        public bool RemoveByAccount(string account)
        {
            return _accountDict.TryRemove(account, out AccountModelDTO outModel);
        }

        public bool IsLogin(string account)
        {
            return _accountDict.TryGetValue(account, out AccountModelDTO outModel);
        }
    }
}
