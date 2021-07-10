using MyWebApi.Definition;

namespace MyWebApi.Model
{
    public class ItemModel
    {
        public sbyte Item_Id { get; set; }
        public ItemType Type { get; set; }
        public sbyte Price { get; set; }
        public short Effect { get; set; }
    }
}
