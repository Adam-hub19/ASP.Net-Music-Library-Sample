using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
    public class RoleController : Controller
    {

        IdentityDbContext _context;
        public RoleController(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RoleIndex()
        {
            var roles = _context.Roles;

            System.Diagnostics.Debug.WriteLine("SomeText");
            return View(await roles.ToListAsync());
        }

        public IActionResult Registerrole()
        {
            //var roles = _roleManager.Roles.ToList();
            return View(new RoleViewModel());
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


    }

}
