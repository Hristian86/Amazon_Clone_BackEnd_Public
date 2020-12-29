namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Services;
    using AkciqApp.ViewModels.OutPutViewModels.Category;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public ActionResult Search([FromQuery] string search)
        {
            try
            {
                var viewModel = this.searchService.FindSearchResults<ProductsInCategoryViewModel>(search);

                return this.Ok(new
                {
                    searchResult = viewModel,
                });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}