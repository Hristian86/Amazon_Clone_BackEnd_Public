using System;
using System.Collections.Generic;
using System.Text;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.UserOrdersHolder
{
    public class OrderProductViewModel : IMapFrom<Product>
    {
        // New properties.
        public string Description { get; set; }

        public int Rating { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public Decimal Price { get; set; }

        // Old properties.
        public string Title { get; set; }

        public string Content { get; set; }
    }
}