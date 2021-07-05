using MyWebApi.Service;

namespace MyWebApi.Model
{
    public class CreateAccountModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class CreateAccountResponse
    { 
        public CreateAccountStatus Status { get; set; }
        public AccountModelDTO Data { get; set; }
    }
}
