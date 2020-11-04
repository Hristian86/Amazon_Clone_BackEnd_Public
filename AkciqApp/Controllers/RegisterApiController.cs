namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AkciqApp.Areas.Identity.Pages.Account;
    using AkciqApp.Models;
    using AkciqApp.Models.Models;
    using AkciqApp.Services;
    using AkciqApp.Services.EmailService;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterApiController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailService email;
        private readonly IUserService userService;
        private readonly IActionContextAccessor accessor;
        private readonly string adminRole = "Admin";
        private readonly string userRole = "User";

        public RegisterApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<Areas.Identity.Pages.Account.RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager,
            IEmailService email,
            IUserService userService,
            IActionContextAccessor accessor)
        {
            _roleManager = roleManager;
            this.email = email;
            this.userService = userService;
            this.accessor = accessor;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OnPostAsync([FromBody] RegisterApiModel Input)
        {
            var returnUrl = Request.GetTypedHeaders().Referer;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (_userManager.Users.Count() == 1)
                {
                    await this._roleManager.CreateAsync(new ApplicationRole { Name = this.adminRole });

                    await this._userManager.AddToRoleAsync(user, adminRole);

                }
                else
                {
                    await this._roleManager.CreateAsync(new ApplicationRole { Name = this.userRole });

                    await this._userManager.AddToRoleAsync(user, userRole);

                }


                if (result.Succeeded)
                {

                    var ip = this.accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();

                    await this.userService.IpAddress(ip, Input.Email);
                    await this.userService.ChangeUserName(Input.Email, Input.UserName);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //email.SendForgottenPass(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}

                    return Ok("User created a new account with password.");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(new Message("rgister fail"));
        }


    }
}