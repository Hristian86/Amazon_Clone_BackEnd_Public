namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Constants;
    using AkciqApp.Data;
    using AkciqApp.Models;
    using AkciqApp.Models.Models;
    using AkciqApp.ViewModels;
    using AkciqApp.ViewModels.CategoryViewHolder;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionContextAccessor accessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        //private readonly string adminRole = "Admin";
        //private readonly string userRole = "User";

        public HomeController(ILogger<HomeController> logger, IActionContextAccessor accessor, ApplicationDbContext _context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _logger = logger;
            this.accessor = accessor;
            this._context = _context;
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            //var user = this._context.Users.FirstOrDefault(x => x.UserName == "asd@asd.bg");
            //var AllUsers = this._context.Users.ToList();

            //var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;
            //var ip = this.accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();

            //var user = this._context.Users.FirstOrDefault(x => x.UserName == "ico221299@gmail.com");
            //var AllUsers = this._context.Users.ToList();

            //var users = this._context.Users.Where(x => x.Email == "ico221299@gmail.com").FirstOrDefault();

            //var user = await userManager.DeleteAsync(users);

            //var allUsers = this._context.Users.ToList();

            //var role = await userManager.GetRolesAsync(users);

            //List<string> roles = new List<string>();
            //roles.Add(this.adminRole);
            //roles.Add(this.userRole);
            //await this.userManager.RemoveFromRolesAsync(users, roles);

            //await this._roleManager.CreateAsync(new IdentityRole { Name = this.adminRole });

            //await this.userManager.AddToRoleAsync(users, adminRole);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code = 404)
        {
            this.ViewData["ErrorMessage"] = $"Error occurred. Error: {code}";
            return this.View("~/Views/Shared/HandleError.cshtml");
        }
    }
}