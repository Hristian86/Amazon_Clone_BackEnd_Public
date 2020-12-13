namespace AkciqApp.Services.CartLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Models.Models;
    using AkciqApp.ViewModels.CartViewHolder;

    public class CartService : CartLogic, ICartService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IDeletableEntityRepository<Product> productRepository;

        private string outOfStock;
        private List<Product> productToBeModified;
        private List<Product> productList;

        public CartService(
            IDeletableEntityRepository<Order> orderRepository,
            IDeletableEntityRepository<Product> productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.outOfStock = "";
            this.productToBeModified = new List<Product>();
            this.productList = new List<Product>();
        }

        public async Task<string> PurchaseMethod(ApplicationUser user, CatrViewModel cartModel)
        {
            try
            {
                base.CheckTheProducts(cartModel, this.productRepository, this.productList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            // Create an order.
            var newOrder = await base.CreateOrder(user, this.orderRepository);

            decimal totalPrice = 0M;

            // Loop the id's from rest api call and send them to the order method.
            foreach (var prod in this.productList)
            {
                // To Do Add quantity property.
                totalPrice += await base.OrderMethod(prod, user, newOrder, this.productToBeModified);
            }

            // Save the updated order.
            newOrder.TotalSum = (double)totalPrice;
            this.orderRepository.Update(newOrder);

            // Modify the quantity of a given product.
            if (this.productToBeModified.Count > 0)
            {
                this.productRepository.UpdateRange(this.productToBeModified);
            }

            await this.orderRepository.SaveChangesAsync();

            // Return the order id;
            return $"id = {newOrder.Id} and total price {totalPrice}," + this.outOfStock;
        }

        // Payment logic...
        public bool PurchasePayment()
        {
            throw new NotImplementedException();
        }
    }
}