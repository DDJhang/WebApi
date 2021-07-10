using MyWebApi.Definition;
using MyWebApi.Model;

namespace MyWebApi.Response
{
    public class GetAccountListResponse
    {
        public AccountStatus Status { get; set; }
        public AccountModelDTO[] Accounts { get; set; }

    }
}
