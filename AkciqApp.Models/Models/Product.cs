namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
            this.OrderItems = new HashSet<OrderItem>();
        }

        // New properties.
        public string Description { get; set; }

        public int Rating { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public Decimal Price { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // Old properties.
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
