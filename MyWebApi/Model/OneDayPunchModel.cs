using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Model
{
    [Table("punch")]    public class OneDayPunchModel
    {
        [Key]
        [Column("account")]
        public string Account { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("punchin")]
        public string PunchIn { get; set; }
        [Column("punchout")]
        public string PunchOut { get; set; }
    }
}
