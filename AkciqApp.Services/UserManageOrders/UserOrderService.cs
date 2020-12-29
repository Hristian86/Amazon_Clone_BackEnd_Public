namespace AkciqApp.Services.UserManageOrders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class UserOrderService : IUserOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public UserOrderService(IDeletableEntityRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IEnumerable<T> GetUserOrders<T>(string userId)
        {
            var userOrders = this.orderRepository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(d => d.CreatedOn)
                .Select(a => a)
                .To<T>()
                .ToList();

            if (userOrders.Count == 0)
            {
                throw new ArgumentNullException("There are no orders for this user");
            }

            return userOrders;
        }
    }
}
