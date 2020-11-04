namespace AkciqApp.ViewModels.OutPutViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class GetByNameViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public int PagesCount { get; set; }

        public int PostsCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<ProductsInCategoryViewModel> ForumPosts { get; set; }
    }
}
