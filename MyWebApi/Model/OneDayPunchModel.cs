using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Model
{
    [Table("punch")]
    public class OneDayPunchModel
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("punchin")]
        public long PunchIn { get; set; }
        [Column("punchout")]
        public long PunchOut { get; set; }
    }
}
