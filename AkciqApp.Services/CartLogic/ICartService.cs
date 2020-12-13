using System.Collections.Generic;
using System.Threading.Tasks;
using AkciqApp.Models.Models;
using AkciqApp.ViewModels.CartViewHolder;

namespace AkciqApp.Services.CartLogic
{
    public interface ICartService
    {
        public Task<string> PurchaseMethod(ApplicationUser user, CatrViewModel cartModel);

        public bool PurchasePayment();
    }
}