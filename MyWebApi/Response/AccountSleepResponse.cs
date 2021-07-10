using MyWebApi.Definition;
using MyWebApi.Model;

namespace MyWebApi.Response
{
    public class AccountSleepResponse
    {
        public AccountStatus Status { get; set; }
        public AccountModelDTO Account { get; set; }
    }
}
