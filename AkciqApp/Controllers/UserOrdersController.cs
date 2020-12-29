namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;
    using AkciqApp.Services.UserManageOrders;
    using AkciqApp.ViewModels.UserOrdersHolder;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserOrdersController : ControllerBase
    {
        private readonly IUserOrderService userOrders;
        private readonly UserManager<ApplicationUser> userManager;

        public UserOrdersController(IUserOrderService userOrders,
            UserManager<ApplicationUser> userManager)
        {
            this.userOrders = userOrders;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserManageOrders()
        {
            try
            {
                // Getting user.
                var usere = this.User.Identity.Name;

                var currUser = await this.userManager.FindByEmailAsync(usere);

                var viewModel = new UserOrderViewModel();
                viewModel.userOrders = this.userOrders.GetUserOrders<OrderViewModel>(currUser.Id);

                return this.Ok(new
                {
                    orders = viewModel,
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(400, ex.Message);
            }
        }
    }
}