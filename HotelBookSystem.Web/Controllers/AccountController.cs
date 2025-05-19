using HotelBookSystem.Application.Common.Interfaces;
using HotelBookSystem.Application.Utility;
using HotelBookSystem.Domain.Entities;
using HotelBookSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBookSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnURL = null)
        {
            returnURL ??= Url.Content("~/");

            LoginVM LoginVM = new()
            {
                Email = string.Empty,
                Password = string.Empty,
                RedirectURL = returnURL
            };

            return View(LoginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["success"] = "Вы успешно вошли в систему";
                    if (!string.IsNullOrEmpty(loginVM.RedirectURL))
                    {
                        return LocalRedirect(loginVM.RedirectURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
            }
            return View(loginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["success"] = "Вы вышли из системы";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register(string? returnURL = null)
        {
            if (!_roleManager.RoleExistsAsync(StaticDetails.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminRole)).Wait();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.CustomerRole)).Wait();
            }

            returnURL ??= Url.Content("~/");

            RegisterVM registerVM = new()
            {
                Email = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                Name = string.Empty,
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                RedirectURL = returnURL
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    Name = registerVM.Name,
                    PhoneNumber = registerVM.PhoneNumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now)
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    if (registerVM.Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.CustomerRole);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["success"] = "Регистрация прошла успешно";

                    if (!string.IsNullOrEmpty(registerVM.RedirectURL))
                    {
                        return LocalRedirect(registerVM.RedirectURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            return View(registerVM);
        }
    }
}