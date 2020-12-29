namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Product> postRepository;

        public SearchService(IDeletableEntityRepository<Product> postRepository)
        {
            this.postRepository = postRepository;
        }

        public IEnumerable<T> FindSearchResults<T>(string searchword)
        {
            IQueryable<Product> products = this.postRepository.All()
                .Where(x => x.Title.ToLower().Contains(searchword.ToLower()))
                .Select(x => x);

            if (products == null)
            {
                throw new ArgumentNullException("No matches");
            }

            return products.To<T>().ToList();
        }
    }
}
