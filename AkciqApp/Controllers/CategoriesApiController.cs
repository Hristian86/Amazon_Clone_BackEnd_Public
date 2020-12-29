namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AkciqApp.Models;
    using AkciqApp.Services;
    using AkciqApp.ViewModels.CategoryViewHolder;
    using AkciqApp.ViewModels.OutPutViewModels.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private const int ItemsPerPage = 5;

        private readonly ICategoryService categoryService;
        private readonly IProductService postService;
        private readonly IHttpClientFactory clientFactory;
        private readonly IActionContextAccessor accessor;
        private readonly IUserService userService;

        public CategoriesApiController(
            ICategoryService categoryService,
            IProductService postService,
            IHttpClientFactory clientFactory,
            IActionContextAccessor accessor,
            IUserService userService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
            this.clientFactory = clientFactory;
            this.accessor = accessor;
            this.userService = userService;
        }

        // GET: Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {

            try
            {
                var requestHeaders = this.Request.Headers.ToList();

                var ip = string.Empty;

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

                // Save the ip address which is requesting this controller.
                await this.userService.SaveIpAddress(ip);

                // Get info for the ip address.
                var request = new HttpRequestMessage(
                    HttpMethod.Get, $"http://api.ipstack.com/" + ip + "?access_key=da76346914acb1dc62e53c323ce12640");
                request.Headers.Add("Accept", "application/json");

                var client = this.clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();
                var getLocation = JsonConvert.DeserializeObject<object>(content);

                // var categories = this.categoryService.GetAll<CategoryVIewModel>();

                // view model for categoryPerant
                var categoryPerant = this.categoryService.GetAllPerants<CategoryPerantViewModel>();

                return this.Ok(new
                {
                    CategoryPerant = categoryPerant,

                    // Categories = categories,
                    GeoLocation = content,
                });
            }
            catch (Exception ex)
            {
                // To Do send message to my email for example
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}