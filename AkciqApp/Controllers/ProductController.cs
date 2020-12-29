namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;
    using AkciqApp.Services;
    using AkciqApp.ViewModels.OutPutViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoryService categories;
        private readonly IProductService productService;

        public ProductController(
            UserManager<ApplicationUser> userManager,
            ICategoryService categories,
            IProductService productService)
        {
            this.userManager = userManager;
            this.categories = categories;
            this.productService = productService;
        }

        public IActionResult ById(int id)
        {
            try
            {
                var viewModel = this.productService.GetById<ProductViewModel>(id);
                return this.Ok(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // To Do send message to my email for example
                return this.StatusCode(500);
            }
        }

        //[Authorize]
        //public IActionResult Create(int? id)
        //{
        //    try
        //    {
        //        var categoriesList = this.categories.GetAll<CategoryDropDownViewModel>();
        //        var viewModel = new ProductCreateInputModel();
        //        viewModel.Categories = categoriesList;
        //        if (id != null)
        //        {
        //            viewModel.Id = (int)id;
        //        }

        //        return this.Ok(viewModel);
        //    }
        //    catch (Exception)
        //    {
        //        // To Do send message to my email for example
        //        return this.StatusCode(500);
        //    }
        //}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                var usere = this.User.Identity.Name;

                var currUser = await this.userManager.FindByEmailAsync(usere);

                var productId = await this.productService.CreateAsync(model, currUser.Id);

                return this.Ok("success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // To Do send message to my email for example
                return this.StatusCode(500);
            }
        }
    }
}