using MyWebApi.Definition;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Model
{
    public class JobModel
    {
        [Key]
        public Job Job_Id { get; set; }
        public short Hp { get; set; }
        public short Mp { get; set; }
        public sbyte Atk { get; set; }
        public sbyte Atk_Max { get; set; }
    }
}
