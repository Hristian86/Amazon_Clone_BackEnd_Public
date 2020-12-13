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

    /// <summary>
    /// Sets the logic for the cart.
    /// </summary>
    public class CartLogic
    {
        /// <summary>
        /// Checking the product if it there and fills the list withe the products.
        /// </summary>
        /// <param name="cartModel">Check.</param>
        /// <param name="productRepository">Product.</param>
        /// <param name="productList">Fils the list.</param>
        protected void CheckTheProducts(
            CatrViewModel cartModel,
            IDeletableEntityRepository<Product> productRepository,
            List<Product> productList,
            int quantity = 2)
        {
            var productIds = cartModel.CartProductIds;
            if (productIds.Count <= 0)
            {
                throw new ArgumentException("No items are provided");
            }

            // Check if the collection of id's have more than 1 quantity, at least one items above 1.
            foreach (var id in productIds)
            {
                var productsToCheck = productRepository.All()
                    .Where(x => x.Id == id).FirstOrDefault();

                if (productsToCheck != null)
                {
                    if (productsToCheck.Quantity - quantity < 0)
                    {
                        throw new ArgumentOutOfRangeException($"{productsToCheck.Title} only {productsToCheck.Quantity} in stock.");
                    }

                    if (productsToCheck.Quantity <= 0)
                    {
                        throw new ArgumentException($"{productsToCheck.Title} is out of stock.");
                    }

                    productList.Add(productsToCheck);
                }
                else
                {
                    throw new ArgumentNullException("Invalid product");
                }
            }
        }

        protected async Task<Order> CreateOrder(ApplicationUser user, IDeletableEntityRepository<Order> orderRepository)
        {
            // Create an order.
            var newOrder = new Order();
            newOrder.User = user;
            newOrder.UserId = user.Id;
            newOrder.Description = "Purchase";

            // Save the order to get the id of it.
            await orderRepository.AddAsync(newOrder);
            await orderRepository.SaveChangesAsync();

            return newOrder;
        }

        protected async Task<decimal> OrderMethod(Product product, ApplicationUser user, Order newOrder, List<Product> productToBeModified, int quantity = 2)
        {
            var price = 0M;
            if (product != null)
            {
                if (product.Quantity - quantity < 0)
                {
                    throw new ArgumentOutOfRangeException($"{product.Title} only {product.Quantity} in stock.");
                }

                if (product.Quantity > 0)
                {
                    // Create new orderItem to be added in the order collection and assign the properties.
                    OrderItem orderItem = new OrderItem();
                    orderItem.Product = product;
                    orderItem.ProductId = product.Id;
                    orderItem.Order = newOrder;
                    orderItem.OrderId = newOrder.Id;
                    orderItem.Price = quantity * product.Price;
                    price = quantity * product.Price;
                    orderItem.Quantity = quantity;
                    newOrder.OrderItems.Add(orderItem);

                    product.Quantity -= quantity;
                    productToBeModified.Add(product);
                }
            }
            else
            {
                throw new ArgumentNullException("Product not found.");
            }

            return price;
        }
    }
}

// var res = this.orderRepository.All()
//                .OrderBy(a => a.CreatedOn)
//                .Select(x => x.OrderItems
//                .Select(a => a.Product)).ToList();