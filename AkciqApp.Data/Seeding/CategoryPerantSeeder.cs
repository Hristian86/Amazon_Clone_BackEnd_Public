namespace AkciqApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;

    public class CategoryPerantSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var addCategoryPerant = new List<CategoryPerant>();

            addCategoryPerant.Add(new CategoryPerant()
            {
                Title = "Mobile phones",
                Name = "Mobile phones",
                ImageUrl = "https://cdn.pixabay.com/photo/2016/07/23/19/15/phone-1537387_960_720.png",
            });

            addCategoryPerant.Add(new CategoryPerant()
            {
                Title = "Watches",
                Name = "Watches",
                ImageUrl = "https://images.unsplash.com/photo-1554151447-b9d2197448f9?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1351&q=80",
            });

            addCategoryPerant.Add(new CategoryPerant()
            {
                Title = "PC Components",
                Name = "PC Components",
                ImageUrl = "https://images.unsplash.com/photo-1602837385569-08ac19ec83af?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=562&q=80",
            });

            foreach (var item in addCategoryPerant)
            {
                await dbContext.CategoryPerants.AddAsync(item);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
