using BookStoreMVCWeb.Models;
using BookStoreMVCWeb.Models.ViewModel;
using BookStoreMVCWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Configuration;

namespace BookStoreMVCWeb.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;
		private ILogger<AccountController> _logger;
		private EmailSender _emailSender;
		private  IConfiguration _configuration { get; }
		//private readonly EmailSender _emailSender;
		public AccountController(SignInManager<AppUser> signInManager,
			UserManager<AppUser> userManager, ILogger<AccountController> logger, IConfiguration configuration, EmailSender emailSender)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_logger = logger;
			_configuration = configuration;
			_emailSender = emailSender;

		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginVm { ReturnUrl = returnUrl });
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVm loginVm)
		{
			if (!ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await
					_signInManager.PasswordSignInAsync(loginVm.UserName, loginVm.Password, false, false);
				//  var user = await _userManager.FindByNameAsync(loginVm.UserName);
				if (result.Succeeded)
				{

					return Redirect(loginVm.ReturnUrl ?? "/");

					//return Redirect(loginVm.ReturnUrl ?? "/admin");
				}
				else if (loginVm.UserName == "admin" && loginVm.Password == "admin123")
				{
					return Redirect(loginVm.ReturnUrl ?? "/admin");
				}
				//else
				//{

				//}
				//else if (loginVm.UserName=="admin" && loginVm.Password=="admin123") 
				//{

				//	return Redirect(loginVm.ReturnUrl ?? "/admin");
				//}

			}
			ModelState.AddModelError("", "Invalid username or password");
			return View(loginVm);
		}
		//private void SendEmail(string email, string subject, string HtmlMessage)
		//{
		//	using (MailMessage mm = new MailMessage(_configuration["NetMail:sender"], email))
		//	{
		//		mm.Subject = subject;
		//		string body = HtmlMessage;
		//		mm.Body = body;
		//		mm.IsBodyHtml = true;
		//		SmtpClient smtp = new SmtpClient();
		//		smtp.Host = _configuration["NetMail:smtpHost"];
		//		smtp.EnableSsl = true;
		//		NetworkCredential NetworkCred = new NetworkCredential(_configuration["NetMail:sender"], _configuration["NetMail:senderpassword"]);
		//		smtp.UseDefaultCredentials = false;
		//		smtp.Credentials = NetworkCred;
		//		smtp.Port = 587;
		//		smtp.Send(mm);
		//	}
			public IActionResult Create()
			{
				return View();
			}
			[HttpPost]
			public async Task<IActionResult> Create(UserVm user)
			{

				if (ModelState.IsValid)
				{
					AppUser newUser = new AppUser
					{
						UserName = user.UserName,
						Email = user.Email,
						PhoneNumber=user.PhoneNumber,
						Opccupation = user.Opccupation,
						EmailConfirmed=true

					};
					IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
					if (result.Succeeded)
					{
				        
                        _logger.LogInformation("User created a new account with password.");
						/*var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
						token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
						var callbackUrl = Url.Page(
					 "/Account/login",
					 pageHandler: null,
					 values: new { area = "Identity", userId = user.Id, token = token, returnUrl = "Clicking me" },
					 protocol: Request.Scheme);
						_emailSender.SendEmail(user.Email, "Confirm your Email",
					 $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/
					TempData["success"] = "Tạo user Thành Công ";
						return Redirect("/account/Login");
					}
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
				return View(user);
			}
			public async Task<IActionResult> LogOut(string ReturnUrl = "/")
			{
				await _signInManager.SignOutAsync();
				return Redirect(ReturnUrl);
			}
			[AllowAnonymous]
			public async Task<IActionResult> ConfirmEmail(string userid, string token)
			{
				if (userid == null || token == null)
				{
					return RedirectToAction("index", "home");
				}

				var user = await _userManager.FindByIdAsync(userid);
				if (user == null)
				{
					ViewBag.ErrorMessage = $"The User ID {userid} is invalid";
					return View("NotFound");
				}

				var result = await _userManager.ConfirmEmailAsync(user, token);
				if (result.Succeeded)
				{
					return View();
				}

				ViewBag.ErrorTitle = "Email cannot be confirmed";
				return View("Error");
			}
		}
	}

