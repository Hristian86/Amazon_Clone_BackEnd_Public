namespace AkciqApp.ViewModels.OutPutViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class CategoryViewModel : IMapFrom<Category>, IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public int PostsCount { get; set; }

        public string URL => $"/f/{this.Name.Replace(' ', '-')}";
    }
}
