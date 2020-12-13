using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkciqApp.Data;
using AkciqApp.Models.Models;
using Xunit;

namespace AkciqApp.Tests
{
    public class CartServiceTest
    {
        private readonly ApplicationDbContext db = new DbContext().getContext();

        [Fact]
        public async Task Purchase_Method_Test_Should_Add_Many_To_Order_Table()
        {
            ApplicationUser user = new ApplicationUser();
            user.Id = "123";
            var listOfIds = new List<int>() { 1, 2, 3, 4 };

            var result = await PurchaseMethod(user, listOfIds);

            Assert.Equal("1", result);

            result = await PurchaseMethod(user, listOfIds);

            Assert.Equal("2", result);

            result = await PurchaseMethod(user, listOfIds);

            Assert.Equal("3", result);
        }

        [Fact]
        public async Task Purchase_Method_Test_ShouldAdd_To_Order_Table()
        {
            ApplicationUser user = new ApplicationUser();
            user.Id = "123";
            var listOfIds = new List<int>() { 1, 2, 3, 4 };

            var result = await PurchaseMethod(user, listOfIds);

            Assert.Equal("1", result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Get_Products_And_Check_Quantity(int quantity)
        {
            var listOfIds = new List<int>() { 1, 2, 3, 4, 5 };

            bool res = await GetProductsAndCheckQuantity<Product>(listOfIds, quantity);

            if (quantity == 1)
            {
                Assert.True(res == true);
            }
            else
            {
                Assert.True(res == false);
            }

        }

        public async Task<bool> GetProductsAndCheckQuantity<T>(List<int> listOfIds, int quantity)
        {

            var result = this.db.Products.Where(x => listOfIds.Contains(x.Id) && (x.Quantity - quantity) >= 0)
                .Select(q => q)
                .ToList();

            if (result.Count == listOfIds.Count)
            {
                return true;
            }

            return false;
        }

        public async Task<string> PurchaseMethod(ApplicationUser user, List<int> productIds)
        {
            // Create an order.
            var newOrder = new Order();
            newOrder.User = user;
            newOrder.UserId = user.Id;
            newOrder.Description = "Purchase";

            // Save the order to get the id of it.
            this.db.Add(newOrder);
            await this.db.SaveChangesAsync();

            // Loop the id's from rest api call and send them to the order method.
            foreach (var id in productIds)
            {
                this.OrderMethod(id, user, newOrder);
            }

            // Save the updated order.
            this.db.Update(newOrder);
            this.db.SaveChanges();

            // Return the order id;
            return $"{newOrder.Id}";
        }

        private void OrderMethod(int id, ApplicationUser user, Order newOrder)
        {
            // Find the current product from id.
            var product = this.db.Products
                .Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                // Create new orderItem to be added in the order collection and assign the properties.
                OrderItem orderItem = new OrderItem();
                orderItem.Product = product;
                orderItem.ProductId = product.Id;
                orderItem.Order = newOrder;
                orderItem.OrderId = newOrder.Id;
                newOrder.OrderItems.Add(orderItem);
            }
            else
            {
                throw new ArgumentNullException("Product not found.");
            }
        }
    }
}
