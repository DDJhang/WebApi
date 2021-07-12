using MyWebApi.Definition;

namespace MyWebApi.Model
{
    public class PunchModel
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public PunchStatus Status { get; set; }
    }
}
