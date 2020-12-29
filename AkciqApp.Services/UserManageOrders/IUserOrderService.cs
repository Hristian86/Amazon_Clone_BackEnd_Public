namespace AkciqApp.Services.UserManageOrders
{
    using System.Collections.Generic;

    public interface IUserOrderService
    {
        IEnumerable<T> GetUserOrders<T>(string userId);
    }
}