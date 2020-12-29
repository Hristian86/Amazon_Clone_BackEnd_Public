namespace AkciqApp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Models.Models;

    public class CategorySeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var addCategories = new List<Category>();

            var allCategoryPerants = dbContext.CategoryPerants.ToList();

            foreach (var categoryPerant in allCategoryPerants)
            {
                if (categoryPerant.Name == "Mobile phones")
                {
                    addCategories.Add(new Category
                    {
                        CategoryPerantId = categoryPerant.Id,
                        Type = "Mobile phones",
                        Name = "Samsung",
                        ImageURL = "https://cdn.pixabay.com/photo/2016/07/23/19/15/phone-1537387_960_720.png",
                        Title = "Mobile phones",
                        Description = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone, is a portable telephone that can make and receive calls over a radio frequency link while the user is moving within a telephone service area. The radio frequency link establishes a connection to the switching systems of a mobile phone operator, which provides access to the public switched telephone network (PSTN). Modern mobile telephone services use a cellular network architecture and, therefore, mobile telephones are called cellular telephones or cell phones in North America. In addition to telephony, digital mobile phones (2G) support a variety of other services, such as text messaging, MMS, email, Internet access, short-range wireless communications (infrared, Bluetooth), business applications, video games and digital photography. Mobile phones offering only those capabilities are known as feature phones; mobile phones which offer greatly advanced computing capabilities are referred to as smartphones.",
                    });
                }

                if (categoryPerant.Name == "Watches")
                {
                    addCategories.Add(new Category
                    {
                        CategoryPerantId = categoryPerant.Id,
                        Type = "Watches",
                        Name = "Wrist Watches",
                        ImageURL = "https://images.unsplash.com/photo-1554151447-b9d2197448f9?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1351&q=80",
                        Title = "Wrist Watches",
                        Description = "Watches progressed in the 17th century from spring-powered clocks, which appeared as early as the 14th century. During most of its history the watch was a mechanical device, driven by clockwork, powered by winding a mainspring, and keeping time with an oscillating balance wheel. These are called mechanical watches.[1][2] In the 1960s the electronic quartz watch was invented, which was powered by a battery and kept time with a vibrating quartz crystal. By the 1980s the quartz watch had taken over most of the market from the mechanical watch. Historically, this is called the quartz revolution (also known as quartz crisis in Swiss).[3][4] Developments in the 2010s include smartwatches, which are elaborate computer-like electronic devices designed to be worn on a wrist. They generally incorporate timekeeping functions, but these are only a small subset of the smartwatch's facilities",
                    });
                }

                if (categoryPerant.Name == "PC Components")
                {
                    addCategories.Add(new Category
                    {
                        CategoryPerantId = categoryPerant.Id,
                        Type = "PC Components",
                        Name = "Video cards",
                        ImageURL = "https://images.unsplash.com/photo-1602837385569-08ac19ec83af?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=562&q=80",
                        Title = "Video cards",
                        Description = "The personal computer is one of the most common types of computer due to its versatility and relatively low price. Desktop personal computers have a monitor, a keyboard, a mouse, and a computer case. The computer case holds the motherboard, fixed or removable disk drives for data storage, the power supply, and may contain other peripheral devices such as modems or network interfaces. Some models of desktop computers integrated the monitor and keyboard into the same case as the processor and power supply. Separating the elements allows the user to arrange the components in a pleasing, comfortable array, at the cost of managing power and data cables between them.",
                    });

                    addCategories.Add(new Category
                    {
                        CategoryPerantId = categoryPerant.Id,
                        Type = "PC Components",
                        Name = "Procesors",
                        ImageURL = "https://s13emagst.akamaized.net/products/30364/30363822/images/res_9d276b96cab0dfa8604db901b6bfe731.jpg?width=450&height=450&hash=3CD60607A16350BE7148357A6EE423AB",
                        Title = "Procesors",
                        Description = "The personal computer is one of the most common types of computer due to its versatility and relatively low price. Desktop personal computers have a monitor, a keyboard, a mouse, and a computer case. The computer case holds the motherboard, fixed or removable disk drives for data storage, the power supply, and may contain other peripheral devices such as modems or network interfaces. Some models of desktop computers integrated the monitor and keyboard into the same case as the processor and power supply. Separating the elements allows the user to arrange the components in a pleasing, comfortable array, at the cost of managing power and data cables between them.",
                    });
                }
            }

            foreach (var category in addCategories)
            {
                await dbContext.Categories.AddAsync(category);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
