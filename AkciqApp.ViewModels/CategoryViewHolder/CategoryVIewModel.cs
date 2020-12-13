using System;
using System.Collections.Generic;
using System.Text;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;
using AkciqApp.ViewModels.OutPutViewModels.Category;

namespace AkciqApp.ViewModels.CategoryViewHolder
{
    public class CategoryVIewModel : IMapFrom<Category>, IMapFrom<Vote>,IMapFrom<Comment>, IMapFrom<Product>, IMapFrom<CategoryPerant>
    {
        public object GeoLocation { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public int ProductsCount { get; set; }

        public int CategoryPerantId { get; set; }

        public IEnumerable<ProductsInCategoryViewModel> Products { get; set; }
    }
}
