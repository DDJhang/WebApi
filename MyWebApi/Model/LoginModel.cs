namespace MyWebApi.Model
{
    public class LoginModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string ErrorMsg { get; set; }
        public AccountModelDTO Data { get; set; }
    }
}
