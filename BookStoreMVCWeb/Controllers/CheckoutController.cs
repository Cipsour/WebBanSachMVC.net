using BookStoreMVCWeb.Data;
using BookStoreMVCWeb.Models;
using BookStoreMVCWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreMVCWeb.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly BookStoreMVCWebContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CheckoutController(BookStoreMVCWebContext context, UserManager<AppUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orderCode = Guid.NewGuid().ToString();
            var orderItem = new Order
            {
                OrderCode = orderCode,
                UserName = userEmail,
                PhoneNumber = user.PhoneNumber, // Lấy PhoneNumber từ AppUser
                Status = 1,
                CreatedDate = DateTime.Now
            };
            _context.Add(orderItem);
            await _context.SaveChangesAsync();

            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            foreach (var cartitem in cartItems)
            {
                var orderDetail = new OrderDeatails
                {
                    UserName = userEmail,
                    OrderCode = orderCode,
                    PhoneNumber = user.PhoneNumber, // Lưu PhoneNumber trong OrderDetails
                    Quantity = cartitem.Quantity,
                    Price = cartitem.Book.Price
                };
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
            }

            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Checkout thành công, chờ duyệt";
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Ordertails()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var viewModel = new OrderDetailsViewModel
            {
                UserName = userEmail,
                PhoneNumber = user.PhoneNumber, // Lấy PhoneNumber từ AppUser
                Email = userEmail,
                CartItems = cartItems
            };
            return View(viewModel);
        }
    }
}
