using BookStoreMVCWeb.Models;
using BookStoreMVCWeb.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager <IdentityRole>_roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager ,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVm model)
        {
           if(ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name=model.RoleName
                };
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id'{Id}'";
                return View("Error");
            }
            EditRoleVm model = new()
            {
                Id = role.Id,
                RoleName = role.Name,

            };
            foreach(var user in _userManager.Users)
            {
                model.Users.Add(user.UserName);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleVm model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id'{model.Id}'";
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            ViewData["roleId"] = id;
            ViewData["roleName"] = role.Name;
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with id '{id}' was found ";
                return View("Error");
            }
            var model = new List<UserRoleVm>();
            foreach(var user in _userManager.Users)
            {
                UserRoleVm userRoleVm = new()
                {
                    Id=user.Id,
                    Name=user.UserName
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVm.IsSelected = true;
                }
                else
                {
                    userRoleVm.IsSelected = false;
                }
                model.Add(userRoleVm);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleVm>model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["ErrorMessage"] = $"No role with Id '{id}' was found";
                return View("Error");
            }
            for(int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].Id);
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected &&(await _userManager.IsInRoleAsync(user, role.Name))){
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }
            return RedirectToAction("Edit", new { Id = id });
        }
    }
}
