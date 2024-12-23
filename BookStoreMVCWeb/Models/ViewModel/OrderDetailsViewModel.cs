namespace BookStoreMVCWeb.Models.ViewModel
{
    public class OrderDetailsViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice => CartItems.Sum(item => item.Book.Price * item.Quantity);
    }
}
