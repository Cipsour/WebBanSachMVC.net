using BookStoreMVCWeb.Data;
using BookStoreMVCWeb.Models;
using BookStoreMVCWeb.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreMVCWeb.Controllers
{
	public class ReviewsController : Controller
	{
		private readonly BookStoreMVCWebContext _context;
		public ReviewsController(BookStoreMVCWebContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult AddReviews(ReviewVm model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userName = User.Identity.Name;
			var review = new Review()
			{
				BookId = model.BookId,
				UserId = userId,
				UserName = userName,
				Comment = model.Comment,
				Rating = model.Rating,
				CreatedDate = DateTime.Now
			};
			_context.Reviews.Add(review);
			_context.SaveChanges();
			return RedirectToAction("Details", "Books", new { id = model.BookId });
		}
	}
}
