using System.ComponentModel.DataAnnotations;
namespace MyWebApi.Model
{
    public class AccountModel
    {
        [Key]
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int Delete { get; set; }
    }
}
