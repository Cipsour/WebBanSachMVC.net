using BookStoreMVCWeb.Data;
using BookStoreMVCWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
namespace BookStoreMVCWeb.Controllers
{
   
    public class CartController : Controller
    {
        private readonly Cart _cart;
       // private readonly IServiceProvider _services;
        private readonly BookStoreMVCWebContext _context;
        public CartController(BookStoreMVCWebContext context,Cart cart)
        {
            _cart = cart;
            _context = context;
            //_services = service;
        }
        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
           
            _cart.CartItems = cartItems;
            return View(cartItems);
        }
        public async Task< IActionResult> AddToCart(int id)
        {
            Book selectedBook = await _context.Books.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItems = cart.Where(x => x.Id == id).FirstOrDefault();
            if (cartItems == null)
            {
                cart.Add(new CartItem
                {
                    Id=id,
                    Book=selectedBook,
                    Quantity=1,
                    CartId=""
                });
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "Thêm thành công ";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(c => c.Id == id).FirstOrDefault();
            if (cartItem.Quantity >= 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(b=>b.Id==id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
			return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(c => c.Id == id).FirstOrDefault();
            if (cartItem.Quantity >=1)
            {
                ++cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(b => b.Id == id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Remove(int Id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(b=>b.Id==Id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Xóa giỏ hàng thành công ";
            return RedirectToAction("Index");
        }
    }
}
