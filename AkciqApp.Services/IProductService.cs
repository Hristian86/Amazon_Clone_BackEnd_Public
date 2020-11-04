namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.ViewModels.OutPutViewModels.Category;
    using AkciqApp.ViewModels.OutPutViewModels.Posts;

    public interface IProductService
    {
        Task<int> CreateAsync(ProductCreateInputModel model, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryID, int? take = null, int skip = 0);

        bool PostExists(int postId);
    }
}
