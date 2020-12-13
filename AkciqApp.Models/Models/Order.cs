namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public string Description { get; set; }

        public double TotalSum { get; set; }

        // and billig stuff ...
    }
}
