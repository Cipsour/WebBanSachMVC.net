using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OpenAI_API;
using OpenAI_API.Completions;
namespace BookStoreMVCWeb.Controllers
{
    public class ChatGPTController : Controller
    {
        [HttpGet]
        public IActionResult UseChatGpt()
        {
            return View();
        }
    }
}
