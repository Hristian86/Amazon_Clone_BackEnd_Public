namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    // not real delete
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int CategoryPerantId { get; set; }

        public virtual CategoryPerant CategoryPerant { get; set; }
    }
}
