using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Agency.DTOs.AccountDto;

namespace MVC_Agency.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

      

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            if(!ModelState.IsValid) return View();
            User user = new User()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerDto);
            }
            await _signInManager.SignInAsync(user,false);
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "yoxdur istifadeci");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user,loginDto.Password,loginDto.IsConfirmed,false);
            return RedirectToAction("Index", "Home");
        }

        //public async  Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole("SuperAdmin");
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    return Ok("Rollar Yarandiiiiiiiiiiii!!!!!!!!!");

        //}

        public async Task<IActionResult> AddRole()
        {
            var role = await _userManager.FindByNameAsync("ramazann.85");
            await _userManager.AddToRoleAsync(role, "SuperAdmin");

            return Ok("Roll verildiiiiii!!!!!!!!!!!");
        }
    }
}
