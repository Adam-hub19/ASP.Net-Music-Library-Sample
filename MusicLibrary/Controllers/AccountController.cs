using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Models;
using MusicLibrary.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        

        private IdentityDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;




        public AccountController (UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            

        }

        

        public IActionResult RoleIndex()
        {
            
            var roles = _roleManager.Roles.ToList();
            var rvm = new List<RoleViewModel>();
            roles.ForEach(item => rvm.Add(
                new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                }
               ));
            return View(rvm);
        }

        public IActionResult Registerrole()
        {
            
            return View(new RoleViewModel());
        }

        public IActionResult AssignRole()
        {
            ViewBag.Name = new SelectList(_roleManager.Roles.ToList(),"Name","Name");
            ViewBag.UserName = new SelectList(_userManager.Users.ToList(), "UserName", "UserName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string UserName, string Name)
        {
            if (!ModelState.IsValid)
                return View();

            // get user
            IdentityUser user = await _userManager.FindByEmailAsync(UserName);

            var result = await _userManager.AddToRoleAsync(user, Name);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("RoleIndex");


        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRole(RoleViewModel role, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View();

            var newRole = new IdentityRole
            {
                Name = role.Name
  
            };

            var result = await _roleManager.CreateAsync(newRole);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("RoleIndex");


        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(

                login.EmailAddress,login.Password,login.RememberMe,false
                );

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "login error");
                return View();
            }

            if(string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("index", "home");

            return Redirect(returnUrl);

        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View();

            await _signInManager.SignOutAsync();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("index", "home");

            return Redirect(returnUrl);

        }

        [AllowAnonymous]
        [HttpPost]
        public async  Task<IActionResult> Register(RegisterViewModel registration)
        {
            if (!ModelState.IsValid)
                return View(registration);

            var newUser = new IdentityUser
            {
                Email = registration.EmailAddress,
                UserName = registration.EmailAddress,
            };

            var result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("Login");

          
        }

    }

}
