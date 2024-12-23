namespace BookStoreMVCWeb.Models
{
   
    public class CartItem
    {
        public CartItem()
        {

        }
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
       
    }
   
}
