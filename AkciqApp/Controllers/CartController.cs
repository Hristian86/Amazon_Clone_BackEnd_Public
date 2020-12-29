namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;
    using AkciqApp.Services.CartLogic;
    using AkciqApp.ViewModels.CartViewHolder;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICartService cartService;

        public CartController(
            UserManager<ApplicationUser> userManager,
            ICartService cartService)
        {
            this.userManager = userManager;
            this.cartService = cartService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase([FromBody] CatrViewModel cartModel)
        {
            try
            {
                // Logic.
                var usere = this.User.Identity.Name;

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

                var currUser = await this.userManager.FindByEmailAsync(usere);

                // Build the cart serice.
                var result = await this.cartService.PurchaseMethod(currUser, cartModel, ip);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(400, ex.Message);
            }
        }
    }
}