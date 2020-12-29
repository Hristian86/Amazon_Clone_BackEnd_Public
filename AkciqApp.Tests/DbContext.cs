using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkciqApp.Data;
using AkciqApp.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AkciqApp.Tests
{
    public class DbContext
    {
        public ApplicationDbContext getContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            ApplicationDbContext db = new ApplicationDbContext(options);

            SeedTotestDb(db);

            return db;
        }

        private void SeedTotestDb(ApplicationDbContext db)
        {
            ApplicationUser user = new ApplicationUser();
            user.Id = "1";

            db.Add(user);
            db.SaveChanges();

            SeedCategory(db);
            SeedCategoryChildren(db);
            SeedProducts(db);
        }

        private void SeedProducts(ApplicationDbContext dbContext)
        {

            var addProducts = new List<Product>();

            var allCategories = dbContext.Categories.ToList();

            foreach (var category in allCategories)
            {
                if (category.Name == "Samsung")
                {
                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy Note 10+ 256 GB (U.S. Warranty), Aura Black/ Note10+",
                        Price = 1100,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61Y6BSxzezL._AC_SL1500_.jpg",
                        Description = @"Connector Type: type-C
Included Components: Screen Protector Leaflet,
                        Smart Switch Insert,
                        Quick Reference Manual,
                        Terms & Conditions / Health & Safety Guide
Camera Description: Rear
Operating System: Android
Wireless Communication Technology: 4G",
                        Content = @"Product Dimensions	0.3 x 2.8 x 5.9 inches
Item Weight	7 ounces
ASIN	B07V4H4FBL
Item model number	SM-N975UZKAXAA
Batteries	1 Lithium ion batteries required. (included)
Customer Reviews	4.6 out of 5 stars    3,362 ratings  
4.6 out of 5 stars
Best Sellers Rank	#9,234 in Cell Phones & Accessories (See Top 100 in Cell Phones & Accessories) 
#116 in Unlocked Cell Phones 
Is Discontinued By Manufacturer	No
OS	Android
RAM	1 GB
Wireless communication technologies	Cellular
Connectivity technologies	Wireless, Bluetooth
Other display features	Wireless
Device interface - primary	Buttons
Other camera features	Rear, Front
Form Factor	Smartphone
Colour	Aura Black
Included Components	Handset, Travel Adapter, Data Cable, AKG Headset, Ejection Pin, USB Connector, S Pen Accessory, Screen Protector Leaflet, Smart Switch Insert, Quick Reference Manual, Terms & Conditions/ Health & Safety Guide
Manufacturer	Samsung
Date First Available	August 7, 2019",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy S8+ 64GB",
                        Price = 450,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61tsDmFOsFL._AC_SL1286_.jpg",
                        Description = @"Service Provider	T-Mobile
Brand   SAMSUNG
Display Size    6.2 Inches
Operating System    Google_android
Memory Storage Capacity",

                        Content = @"About this item
Unlocked cell phones are compatible with GSM carrier such as AT&T and T-Mobile, but are not compatible with CDMA carriers such as Verizon and Sprint.
Infinity Display: a bezel-less, full-frontal, edge-to-edge screen. Default resolution is Full HD+ and can be changed to Quad HD+ (WQHD+) in Settings
Camera resolution - Front: 8 MP AF, Rear: 12 MP OIS AF
Memory: Internal Memory 64 GB, RAM 4GB.
International version can be used with all GSM carriers worldwide,
                        including the Americas: North America,
                        Central and South America and the Caribbean",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy S9+ 64GB",
                        Price = 12,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81VnTSgQgQL._AC_SL1500_.jpg",
                        Description = @"Service Provider	Unlocked
Brand   Samsung Electronics
Display Size    6.2 Inches
Operating System    Android 8.0
Memory Storage Capacity 64 GB",

                        Content = @"About this item
Super Speed Dual Pixel Camera with Rear Dual Camera
Infinity Display: edge to edge immersive screen,
                        enhancing your entertainment experience
IP68 rating: withstands splashes,
                        spills,
                        and rain so it can take a dip,
                        worry free
Internal Memory 64 GB.Expandable Storage up to 400GB
Fast Wireless Charging: Avoid the wires and power up quickly by placing your phone on a Fast Wireless Charger",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy S10+ 128GB  Prism Black",
                        Price = 849.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61eCdKZk17L._AC_SL1500_.jpg",
                        Description = @"Cinematic display
A nearly frameless Cinematic Infinity Display for more detail and clarity, more immersive and uninterrupted content, in a slim, balanced form.",
                        Content = @"Updated Camera Features: Get the more powerful S10 with a software update that gives you all new features including Single Take AI, Pro Video and more
High - quality camera lenses: With a full set of pro lenses,
                        including ultrawide for stunning landscapes and micro - zoom for epic details, Galaxy S10 + is a studio in your pocket, featuring live video bokeh, precision audio focus and super - stabilization
Photos and video with one tap: capture multiple images and video all at once, in one tap.Lenses, effects and filters capture the best of every moment, every time
Capture with pro - grade tool set made for masters: Control your camera settings with Pro - Video Mode to adjust the exposure, focus and more to bring your creative vision to life
Share more, quicker: The Galaxy S10 + can now connect to and share with up to 5 other compatible devices at the same time using Bluetooth and WiFi, with no limits on content type or file size
Sleek Design: Samsung Galaxy S10 + boasts a super-slim design that fits comfortably in your pocket and easily sits in the hand. A 6.4” endless quad HD + dynamic AMOLED screen with nearly bezel - less infinity display offers a cinematic viewing experience
All - day Battery: Fast - charging, long-lasting intelligent power features super-speed processing, Wireless PowerShare, and massive storage, and tailors battery usage to how you live and work to optimize battery life to last all day",
                    });


                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy S10+ F 128GB  Blue",
                        Price = 849.99M,
                        Quantity = 1,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51x5ilwAPmL._AC_SL1001_.jpg",
                        Description = @"Cinematic display
A nearly frameless Cinematic Infinity Display for more detail and clarity, more immersive and uninterrupted content, in a slim, balanced form.",
                        Content = @"Updated Camera Features: Get the more powerful S10 with a software update that gives you all new features including Single Take AI, Pro Video and more
High - quality camera lenses: With a full set of pro lenses,
                        including ultrawide for stunning landscapes and micro - zoom for epic details, Galaxy S10 + is a studio in your pocket, featuring live video bokeh, precision audio focus and super - stabilization
Photos and video with one tap: capture multiple images and video all at once, in one tap.Lenses, effects and filters capture the best of every moment, every time
Capture with pro - grade tool set made for masters: Control your camera settings with Pro - Video Mode to adjust the exposure, focus and more to bring your creative vision to life
Share more, quicker: The Galaxy S10 + can now connect to and share with up to 5 other compatible devices at the same time using Bluetooth and WiFi, with no limits on content type or file size
Sleek Design: Samsung Galaxy S10 + boasts a super-slim design that fits comfortably in your pocket and easily sits in the hand. A 6.4” endless quad HD + dynamic AMOLED screen with nearly bezel - less infinity display offers a cinematic viewing experience
All - day Battery: Fast - charging, long-lasting intelligent power features super-speed processing, Wireless PowerShare, and massive storage, and tailors battery usage to how you live and work to optimize battery life to last all day",
                    });


                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Samsung Galaxy S10+ 128GB  White",
                        Price = 849.99M,
                        Quantity = 1,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51Fvxm3KzXL._AC_SL1500_.jpg",
                        Description = @"Cinematic display
A nearly frameless Cinematic Infinity Display for more detail and clarity, more immersive and uninterrupted content, in a slim, balanced form.",
                        Content = @"Updated Camera Features: Get the more powerful S10 with a software update that gives you all new features including Single Take AI, Pro Video and more
High - quality camera lenses: With a full set of pro lenses,
                        including ultrawide for stunning landscapes and micro - zoom for epic details, Galaxy S10 + is a studio in your pocket, featuring live video bokeh, precision audio focus and super - stabilization
Photos and video with one tap: capture multiple images and video all at once, in one tap.Lenses, effects and filters capture the best of every moment, every time
Capture with pro - grade tool set made for masters: Control your camera settings with Pro - Video Mode to adjust the exposure, focus and more to bring your creative vision to life
Share more, quicker: The Galaxy S10 + can now connect to and share with up to 5 other compatible devices at the same time using Bluetooth and WiFi, with no limits on content type or file size
Sleek Design: Samsung Galaxy S10 + boasts a super-slim design that fits comfortably in your pocket and easily sits in the hand. A 6.4” endless quad HD + dynamic AMOLED screen with nearly bezel - less infinity display offers a cinematic viewing experience
All - day Battery: Fast - charging, long-lasting intelligent power features super-speed processing, Wireless PowerShare, and massive storage, and tailors battery usage to how you live and work to optimize battery life to last all day",
                    });
                }

                // Wathes.
                if (category.Name == "Wrist Watches")
                {
                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Mens Watches Ultra-Thin Minimalist Waterproof",
                        Price = 28.96M,
                        Quantity = 1,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61VD2rzNhxL._AC_UL1200_.jpg",
                        Description = @"Brand	FIZILI
Clasp type	Tang Buckle
Band Width	2 Centimeters
Case Diameter	4 Centimeters
Case Thickness	0.7 Centimeters",
                        Content = @"Ultra Thin Soft Stainless Steel Mesh Band
Steel braided watchband adjustable and suitable for 99.99% wrists. (No hurt to body hairs on wrists)

Easy to Adjustment of Watchband

In this image, by the adjustment tool (gift in our fizili package), anyone can finish the adjustment of watchband within 1 minute.

NOTE: In step 3, be sure the clasp be fasten in the notch on watchband surface, or it maybe difficult to close the clasp, and also easy to cause slipping off from your wrist.",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Mens Watches Ultra-Thin Minimalist Waterproof",
                        Price = 38.96M,
                        Quantity = 1,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61qLomhVJHL._AC_UX679_.jpg",
                        Description = @"Brand	FIZILI
Clasp type	Tang Buckle
Band Width	2 Centimeters
Case Diameter	4 Centimeters
Case Thickness	0.7 Centimeters",
                        Content = @"Ultra Thin Soft Stainless Steel Mesh Band
Steel braided watchband adjustable and suitable for 99.99% wrists. (No hurt to body hairs on wrists)

Easy to Adjustment of Watchband

In this image, by the adjustment tool (gift in our fizili package), anyone can finish the adjustment of watchband within 1 minute.

NOTE: In step 3, be sure the clasp be fasten in the notch on watchband surface, or it maybe difficult to close the clasp, and also easy to cause slipping off from your wrist.",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Mens Watches Ultra-Thin Minimalist Waterproof",
                        Price = 149.00M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71ps8deaD6L._AC_UL1500_.jpg",
                        Description = @"Brand	Stauer
Band Material Type	Leather
Watch Movement	Automatic
Case Material	Stainless Steel
Display	Analog",
                        Content = @"About this item
Four interior dials - Day, Date, 24 hour clock and one that Depicts The Sun and Moon
Stainless Steel case
Automatic Movement - functions on kinetic power (simple body movement)
Croc-embossed genuine brown leather band Fits wrists
Keep your automatic watches running in style with one of our Watch Winders which can be found in the Automatic Watch section of our Stauer Amazon brand store page.",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "Men’s Watch Mechanical Stainless Steel Skeleton Waterproof",
                        Price = 38.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81CyFBUKU4L._AC_UX679_.jpg",
                        Description = @"Brand	FIZILI
Clasp type	Tang Buckle
Band Width	2 Centimeters
Case Diameter	4 Centimeters
Case Thickness	0.7 Centimeters",
                        Content = @"Ultra Thin Soft Stainless Steel Mesh Band
Steel braided watchband adjustable and suitable for 99.99% wrists. (No hurt to body hairs on wrists)

Easy to Adjustment of Watchband

In this image, by the adjustment tool (gift in our fizili package), anyone can finish the adjustment of watchband within 1 minute.

NOTE: In step 3, be sure the clasp be fasten in the notch on watchband surface, or it maybe difficult to close the clasp, and also easy to cause slipping off from your wrist.",
                    });

                }

                if (category.Name == "Video cards")
                {
                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "EVGA GeForce GTX 980 4GB Graphics Card 04G-P4-5988-KR",
                        Price = 399.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71Z7xD1%2BhyL._AC_SL1200_.jpg",
                        Description = @"Brand	EVGA
Graphics Coprocessor	Nvidia GeForce
Chipset Brand	Nvidia
Graphics Ram Type	GDDR5
Graphics Ram Size	4 GB",
                        Content = @"About this item
Virtual Reality Ready
DirectX12 Ready
Gamestream to NVIDIA SHIELD
EVGA GTX 980 K | NGP | N Overclocking Perfection,
                        14 + 3 Phase Fully Digital VRM
EVGA's 24/7 Technical Support; Base Clock: 1304 MHz / Boost Clock: 1418 MHz
Memory Clock: 7010 MHz Effective; CUDA Cores: 2048; Memory Detail: 4096MB GDDR5
Memory Bit Width 256 Bit / Memory Speed: 0.28ns / Memory Bandwidth: 224.3 GB / s
Recommended PSU: 500W or greater power supply",
                    });


                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "EVGA GeForce GTX 980 4GB  Graphics Card 04G-P4-5988-KR",
                        Price = 399.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/715xSIgx05L._AC_SL1200_.jpg",
                        Description = @"Brand	EVGA
Graphics Coprocessor	Nvidia GeForce
Chipset Brand	Nvidia
Graphics Ram Type	GDDR5
Graphics Ram Size	4 GB",
                        Content = @"About this item
Virtual Reality Ready
DirectX12 Ready
Gamestream to NVIDIA SHIELD
EVGA GTX 980 K | NGP | N Overclocking Perfection,
                        14 + 3 Phase Fully Digital VRM
EVGA's 24/7 Technical Support; Base Clock: 1304 MHz / Boost Clock: 1418 MHz
Memory Clock: 7010 MHz Effective; CUDA Cores: 2048; Memory Detail: 4096MB GDDR5
Memory Bit Width 256 Bit / Memory Speed: 0.28ns / Memory Bandwidth: 224.3 GB / s
Recommended PSU: 500W or greater power supply",
                    });


                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "EVGA GeForce GTX 980 4GB  Graphics Card 04G-P4-5988-KR",
                        Price = 399.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/616RdQsA9vL._AC_SL1200_.jpg",
                        Description = @"Brand	EVGA
Graphics Coprocessor	Nvidia GeForce
Chipset Brand	Nvidia
Graphics Ram Type	GDDR5
Graphics Ram Size	4 GB",
                        Content = @"About this item
Virtual Reality Ready
DirectX12 Ready
Gamestream to NVIDIA SHIELD
EVGA GTX 980 K | NGP | N Overclocking Perfection,
                        14 + 3 Phase Fully Digital VRM
EVGA's 24/7 Technical Support; Base Clock: 1304 MHz / Boost Clock: 1418 MHz
Memory Clock: 7010 MHz Effective; CUDA Cores: 2048; Memory Detail: 4096MB GDDR5
Memory Bit Width 256 Bit / Memory Speed: 0.28ns / Memory Bandwidth: 224.3 GB / s
Recommended PSU: 500W or greater power supply",
                    });

                    addProducts.Add(new Product
                    {
                        CategoryId = category.Id,
                        Category = category,
                        Title = "EVGA GeForce GTX 980 4GB  Graphics Card 04G-P4-5988-KR",
                        Price = 399.99M,
                        Quantity = 100,
                        Rating = 1,
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81Nn%2BtFCkDL._AC_SL1500_.jpg",
                        Description = @"Brand	EVGA
Graphics Coprocessor	Nvidia GeForce
Chipset Brand	Nvidia
Graphics Ram Type	GDDR5
Graphics Ram Size	4 GB",
                        Content = @"About this item
Virtual Reality Ready
DirectX12 Ready
Gamestream to NVIDIA SHIELD
EVGA GTX 980 K | NGP | N Overclocking Perfection,
                        14 + 3 Phase Fully Digital VRM
EVGA's 24/7 Technical Support; Base Clock: 1304 MHz / Boost Clock: 1418 MHz
Memory Clock: 7010 MHz Effective; CUDA Cores: 2048; Memory Detail: 4096MB GDDR5
Memory Bit Width 256 Bit / Memory Speed: 0.28ns / Memory Bandwidth: 224.3 GB / s
Recommended PSU: 500W or greater power supply",
                    });
                }
            }

            foreach (var products in addProducts)
            {
                dbContext.Products.Add(products);
            }

            dbContext.SaveChanges();
        }

        private void SeedCategoryChildren(ApplicationDbContext dbContext)
        {

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
                dbContext.Categories.Add(category);
            }

            dbContext.SaveChanges();
        }

        private void SeedCategory(ApplicationDbContext dbContext)
        {
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
                dbContext.CategoryPerants.Add(item);
            }

            dbContext.SaveChanges();
        }
    }
}