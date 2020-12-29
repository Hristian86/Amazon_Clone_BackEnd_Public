namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class CategoryPerant : BaseDeletableModel<int>
    {
        public CategoryPerant()
        {
            this.Categories = new HashSet<Category>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
