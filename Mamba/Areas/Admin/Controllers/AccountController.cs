using Core.Models;
using Mamba.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            User user=new User()
            {
                Name = registerDto.Name,
                Surname= registerDto.Surname,
                Email = registerDto.Email,
                UserName=registerDto.UserName
            };
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            Console.WriteLine("Salam");
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index","Dashboard");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.EmailOrUserName);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.EmailOrUserName);
                if(user == null)
                {
                    ModelState.AddModelError("", "UsernameOrEmail ve ya password duzgun daxil edilmeyib");
                    return View();
                }
                
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin!");
                return View();
            }
            if(!result.Succeeded) {
                ModelState.AddModelError("", "UsernameOrEmail ve ya password duzgun daxil edilmeyib");
                return View();
            }
            await _signInManager.SignInAsync(user,loginDto.IsRemember);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
