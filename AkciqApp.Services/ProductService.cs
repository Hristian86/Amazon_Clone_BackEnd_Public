namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;
    using AkciqApp.ViewModels.OutPutViewModels.Category;
    using AkciqApp.ViewModels.OutPutViewModels.Posts;

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> postRepository;

        public ProductService(IRepository<Product> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<int> CreateAsync(ProductCreateInputModel model, string userId)
        {
            Product product = new Product()
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Content = model.Content,
                UserId = userId,
                Description = model.Content,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Rating = model.Rating,
            };

            await this.postRepository.AddAsync(product);
            await this.postRepository.SaveChangesAsync();
            return product.Id;
        }

        public T GetById<T>(int id)
        {
            var post = this.postRepository.All().Where(x => x.Id == id);
            if (post == null)
            {
                throw new ArgumentNullException("The post is not found");
            }

            return post.To<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryID, int? take = null, int skip = 0)
        {
            IQueryable<Product> query = this.postRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryID)
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public bool PostExists(int postId)
        {
            bool exists = this.postRepository.All()
                .Any(x => x.Id == postId);
            return exists;
        }
    }
}
