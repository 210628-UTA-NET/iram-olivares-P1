
namespace SAModels
{
    public class OrderItem : Item
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}