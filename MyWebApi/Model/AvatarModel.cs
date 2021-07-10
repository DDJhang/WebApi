using MyWebApi.Definition;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Model
{
    public class AvatarModel
    {
        [Key]
        public short Avatar_Id { get; set; }
        public string Account_Key { get; set; }
        public string Name { get; set; }
        public Job Job { get; set; }
        public short Hp { get; set; }
        public short Max_Hp { get; set; }
        public short Mp { get; set; }
        public short Max_Mp { get; set; }
        public short Atk_Min { get; set; }
        public short Atk_Max { get; set; }
        public int Money { get; set; }
        public short Delete { get; set; }
    }
}
