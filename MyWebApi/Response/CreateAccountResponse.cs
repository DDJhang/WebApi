using MyWebApi.Definition;
using MyWebApi.Model;

namespace MyWebApi.Response
{
    public class CreateAccountResponse
    {
        public AccountStatus Status { get; set; }
        public AccountModelDTO Data { get; set; }
    }
}
