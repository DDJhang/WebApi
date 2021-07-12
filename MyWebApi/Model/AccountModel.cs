using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Model
{
    [Table("accounts")]
    public class AccountModel
    {
        [Key]
        [Column("account")]
        public string Account { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("inactive")]
        public int Delete { get; set; }
    }
}
