using BookStoreMVCWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly BookStoreMVCWebContext _context;

        public OrderController(BookStoreMVCWebContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.Orders.OrderByDescending(p=>p.Id).ToListAsync());
        }
        public async Task<IActionResult>ApproveOrder(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode == id);
            if (order == null)
            {
                return NotFound();
            }
            order.IsApproved = true;
            order.IsCanceled = false;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CancelOrder(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderCode == id);
            if (order == null)
            {
                return NotFound();
            }
            order.IsApproved = false;
            order.IsCanceled = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
