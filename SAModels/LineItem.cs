
namespace SAModels
{
    public class LineItem : Item
    {
        public int LineItemID { get; set; }
        public int StoreFrontID { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}