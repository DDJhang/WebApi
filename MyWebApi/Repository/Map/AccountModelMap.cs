using Dapper.FluentMap.Mapping;
using MyWebApi.Model;

namespace MyWebApi.Repository.Map
{
    public class AccountModelMap: EntityMap<AccountModel>
    {
        public AccountModelMap()
        {
            Map(p => p.Account).ToColumn("account");
            Map(p => p.Password).ToColumn("password");
            Map(p => p.Name).ToColumn("name");
            Map(p => p.Delete).ToColumn("inactive");
        }
    }
}
