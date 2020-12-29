using System;
using System.Collections.Generic;
using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.UserOrdersHolder
{
    // Order view model.
    public class OrderViewModel : IMapFrom<Order>
    {
        public double TotalSum { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<OrderItemsViewModel> OrderItems { get; set; }
    }
}