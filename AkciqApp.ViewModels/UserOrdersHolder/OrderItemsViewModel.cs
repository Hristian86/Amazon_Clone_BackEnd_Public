using AkciqApp.Mapping;
using AkciqApp.Models.Models;

namespace AkciqApp.ViewModels.UserOrdersHolder
{
    // Order items view model.
    public class OrderItemsViewModel : IMapFrom<OrderItem>, IMapFrom<Product>
    {
        public int productId { get; set; }

        public OrderProductViewModel Product { get; set; }
    }
}