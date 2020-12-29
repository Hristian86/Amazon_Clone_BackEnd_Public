namespace AkciqApp.ViewModels.OutPutViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Web.Mvc;

    public class ProductCreateInputModel
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [AllowHtml]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        // public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public Decimal Price { get; set; }
    }
}
