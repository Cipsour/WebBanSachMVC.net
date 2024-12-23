 using BookStoreMVCWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
namespace BookStoreMVCWeb.Models
{
    public class Cart
    {
        private readonly BookStoreMVCWebContext _context;
       // private readonly IHttpContextAccessor _httpContextAccessor;
        public Cart(BookStoreMVCWebContext context)
        {
           // _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public static Cart GetList(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = services.GetService<BookStoreMVCWebContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
            session.SetString("Id", cartId);
            
            return new Cart(context) { Id = cartId };
        } 
      
        public List<CartItem> GetAllCartItems()
        {
           
            return CartItems ?? (CartItems = _context.CartItems.Where(ci => ci.CartId == Id).
             Include(ci => ci.Book).ToList()
                );
        }
        public CartItem GetCartItem(Book book)
        {
            return _context.CartItems.SingleOrDefault
				(ci => ci.Book.Id == book.Id && ci.CartId == Id);
        }
        
      
        public void AddToCart(Book book,int quantity)
        {
            var cartitem = GetCartItem(book);
            if (cartitem == null) 
            {
                cartitem = new CartItem
                {
                  Book=book,
                  Quantity=quantity,
                  CartId=Id
                };
                _context.CartItems.Add(cartitem);
            }
            else
            {
                cartitem.Quantity += quantity;
            }
          
            _context.SaveChanges();
        }
        public int GetCartTotal()
        {
            return _context.CartItems.Where(ci => ci.CartId == Id)
                .Select(ci => ci.Book.Price * ci.Quantity)
                .Sum();
        }

    }
}
