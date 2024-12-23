using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVCWeb.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
