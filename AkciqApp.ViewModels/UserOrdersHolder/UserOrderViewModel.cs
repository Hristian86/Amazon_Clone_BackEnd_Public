using System;
using System.Collections.Generic;
using System.Text;

namespace AkciqApp.ViewModels.UserOrdersHolder
{
    public class UserOrderViewModel
    {
        public IEnumerable<OrderViewModel> userOrders { get; set; }
    }
}
