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

                var currUser = await this.userManager.FindByEmailAsync(usere);

                // Build the cart serice.
                var result = await this.cartService.PurchaseMethod(currUser, cartModel);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(400, ex.Message);
            }
        }
    }
}