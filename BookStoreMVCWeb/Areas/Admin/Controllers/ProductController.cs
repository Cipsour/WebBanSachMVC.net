using BookStoreMVCWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVCWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
    public class ProductController : Controller
	{
		private readonly BookStoreMVCWebContext _context;
		public ProductController(BookStoreMVCWebContext context)
		{
			_context = context;
		}
        public async Task< IActionResult> Index()
		{
			return View(await _context.Books.OrderByDescending(p => p.Id).ToListAsync());
		}
		
	}
}
