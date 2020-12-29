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
    using AkciqApp.ViewModels.CategoryViewHolder;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;

    [Route("api/[controller]")]
    [ApiController]
    public class LogInApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<Areas.Identity.Pages.Account.LoginModel> _logger;
        private readonly IAntiforgery antiforgery;
        private readonly IUserService userService;
        private string role;

        public LogInApiController(SignInManager<ApplicationUser> signInManager,
            ILogger<Areas.Identity.Pages.Account.LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IAntiforgery antiforgery,
            IUserService userService
            )
        {
            this.antiforgery = antiforgery;
            this.userService = userService;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IAntiforgery Antiforgery { get; }

        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        // [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginApiModel model)
        {

            var tokens = antiforgery.GetAndStoreTokens(HttpContext);

            HttpContext.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken, new CookieOptions()
            {
                HttpOnly = false,
                Secure = false,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
            });

            //return new ObjectResult(new
            //{
            //    token = tokens.RequestToken,
            //    tokenName = tokens.HeaderName
            //});

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
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

                await this.userService.IpAddress(ip, model.Email);

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                this.role = userRoles[0];
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return this.Ok(new
                {
                    role = this.role,
                    user = user.UserName,
                    email = user.Email,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    XSRFToken = tokens.RequestToken,
                    tokenName = tokens.HeaderName,
                });
            }

            return this.Unauthorized();
        }
    }
}