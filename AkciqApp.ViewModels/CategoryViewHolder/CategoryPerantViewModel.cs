using System;
using System.Collections.Generic;
using System.Text;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.CategoryViewHolder
{
    public class CategoryPerantViewModel : IMapFrom<CategoryPerant>, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Delimeter { get; set; } = "Perant";

        public string ImageUrl { get; set; }

        public int CategoriesCount { get; set; }

        public IEnumerable<CategoryVIewModel> Categories { get; set; }
    }
}
