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

        public CategoriesApiController(
            ICategoryService categoryService,
            IProductService postService,
            IHttpClientFactory clientFactory,
            IActionContextAccessor accessor)
        {
            this.categoryService = categoryService;
            this.postService = postService;
            this.clientFactory = clientFactory;
            this.accessor = accessor;
        }

        //[HttpGet]
        //[Authorize]
        //public ActionResult ByName(string name, int? page)
        //{
        //    if (!page.HasValue || page <= 0)
        //    {
        //        page = 1;
        //    }

        //    try
        //    {
        //        var viewModel = this.categoryService.GetByName<GetByNameViewModel>(name);
        //        var count = viewModel.PostsCount;

        //        // Preventing exceptions for pagination
        //        if (count == 0)
        //        {
        //            return this.Ok(viewModel);
        //        }

        //        var pageCount = (int)Math.Ceiling((double)count / ItemsPerPage);
        //        if (page > pageCount)
        //        {
        //            page = pageCount;
        //        }

        //        viewModel.PagesCount = pageCount;

        //        viewModel.ForumPosts = this.postService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (int)((page - 1) * ItemsPerPage));
        //        viewModel.CurrentPage = (int)page;

        //        if (viewModel == null)
        //        {
        //            return this.RedirectToAction("Index", "Categories");
        //        }

        //        return this.Ok(viewModel);
        //    }
        //    catch (Exception)
        //    {
        //        // To Do send message to my email for example
        //        return this.RedirectToAction("HandleError", "Home");
        //    }
        //}

        // GET: Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var requestHeaders = this.Request.Headers.ToList();

            var ip = this.accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();

            var request = new HttpRequestMessage(
                HttpMethod.Get, $"http://api.ipstack.com/" + ip +"?access_key=da76346914acb1dc62e53c323ce12640");
            request.Headers.Add("Accept", "application/json");

            var client = this.clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var getLocation = JsonConvert.DeserializeObject<object>(content);

            try
            {
                var viewModel = new CategoryProectionViewModel();

                var categories = this.categoryService.GetAll<CategoryVIewModel>();

                return this.Ok(new
                {
                    Categories = categories,
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