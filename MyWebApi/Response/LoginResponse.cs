using MyWebApi.Model;

namespace MyWebApi.Response
{
    public class LoginResponse
    {
        public string ErrorMsg { get; set; }
        public AccountModelDTO Data { get; set; }
    }
}
