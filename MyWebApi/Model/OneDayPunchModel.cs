using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Model
{
    public class OneDayPunchModel
    {
        [Key]
        public string Account { get; set; }
        public string Name { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
    }
}
