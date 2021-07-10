using MyWebApi.Definition;

namespace MyWebApi.Model
{
    public class CreateAvatarModel
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public Job Job { get; set; }
    }
}
