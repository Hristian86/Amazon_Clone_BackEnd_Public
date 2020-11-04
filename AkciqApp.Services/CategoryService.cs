namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoryRepository.All()
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoryRepository.All()
                .Where(x => x.Name == name)
                .To<T>()
                .FirstOrDefault();
            if (category == null)
            {
                throw new ArgumentNullException("The category name is not found");
            }

            return category;
        }
    }
}
