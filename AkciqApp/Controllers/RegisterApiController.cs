namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AkciqApp.Areas.Identity.Pages.Account;
    using AkciqApp.Models;
    using AkciqApp.Models.Models;
    using AkciqApp.Services;
    using AkciqApp.Services.EmailService;
    using AngleSharp;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

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
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly string adminRole = "Admin";
        private readonly string userRole = "User";
        private IList<string> errorsInResult;

        public RegisterApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<Areas.Identity.Pages.Account.RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager,
            IEmailService email,
            IUserService userService,
            IActionContextAccessor accessor,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _roleManager = roleManager;
            this.email = email;
            this.userService = userService;
            this.accessor = accessor;
            this._configuration = configuration;
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
                var userNameCheck = this.userService.UserNameExists(Input.UserName);

                if (userNameCheck)
                {
                    return this.BadRequest(new ErrorOutputModel($"User name '{Input.UserName}' is already taken."));
                }

                var emailCheck = await this._userManager.FindByEmailAsync(Input.Email);

                if (emailCheck != null)
                {
                    return this.BadRequest(new ErrorOutputModel($"Email '{Input.Email}' is already taken."));
                }

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
                    string ip = string.Empty;

                    // Heroku ip address.
                    string ipAdr = this.Request.Headers["x-forwarded-for"];

                    if (ipAdr != null)
                    {
                        var ips = ipAdr.Split(",");
                        ip = ips[ips.Length - 1];
                    }
                    else
                    {
                        ip = this.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                        ip = "212.25.57.243";
                    }

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

                    return this.Ok("User created");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    this.errorsInResult.Add(error.Description);
                }

                if (this.errorsInResult.Any())
                {
                    return this.BadRequest(new ErrorOutputModel(this.errorsInResult));
                }
            }

            // If we got this far, something failed, redisplay form
            return this.BadRequest(new ErrorOutputModel("Email is taken"));
        }
    }
}