namespace AkciqApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Data;
    using AkciqApp.Data.Repositories;
    using AkciqApp.Data.Seeding;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;
    using AkciqApp.Services;
    using AkciqApp.Services.EmailService;
    using AkciqApp.ViewModels;
    using ForumSys.Data;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        private readonly IConfiguration configuration;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private string envVariable = "DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddCors(option =>
            {
                option.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44342")
                        .AllowAnyHeader();

                        builder.WithOrigins("https://resttesttest.com")
                        .AllowAnyHeader();
                        //netlify con
                        builder.WithOrigins("https://inspiring-keller-950b4a.netlify.app")
                        .AllowAnyHeader();

                        builder.WithOrigins("https://sweetycons.tk")
                        .AllowAnyHeader();
                        //end of netlify

                        //vercel app
                        builder.WithOrigins("https://sweetycons.ga")
                        .AllowAnyHeader();

                        builder.WithOrigins("https://react-akcia-phbh8h2jz.vercel.app")
                        .AllowAnyHeader();
                        //end of vercel app

                        //firebse app
                        builder.WithOrigins("https://sweetycons.gq")
                        .AllowAnyHeader();

                        builder.WithOrigins("https://tombol-a.web.app")
                        .AllowAnyHeader();
                    //end of firebase app

                        builder.WithOrigins("https://web.postman.co")
                        .AllowAnyHeader();

                        builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();

                //builder.WithOrigins("https://localhost:5001").AllowAnyHeader();

            });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddHttpClient();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
            });

            //// sessions
            //services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = false;
            //});

            // jwt bearer options
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer.
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            //services.AddControllersWithViews();
            services.AddSingleton(this.configuration);
            services.AddRazorPages();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();


            // Services
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IEmailService>(x => new EmailService());
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();

            services.Configure<CookiePolicyOptions>(
            options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // Anti forgery token header.
            services.AddAntiforgery(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "XSRF-TOKEN";
                //options.SuppressXFrameOptionsHeader = false;
            });

            //// Autovalideta token
            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                this.envVariable = "DevConnection";
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                this.envVariable = "DefaultConnection";
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // app.UseHttpsRedirection();
            app.UseForwardedHeaders();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseSession();

            ///* Configure the app to provide a token in a cookie called XSRF-TOKEN */
            ///* Custom Middleware Component is required to Set the cookie which is named CSRF-TOKEN 
            // * The Value for this cookie is obtained from IAntiForgery service
            // * We must configure this cookie with HttpOnly option set to false so that browser will allow JS to read this cookie
            // */
            //app.Use(nextDelegate => context =>
            //{
            //    string path = context.Request.Path.Value.ToLower();
            //    string[] directUrls = { "/admin", "/store", "/cart", "checkout", "/login" };
            //    if (path.StartsWith("/swagger") || path.StartsWith("/api") || string.Equals("/", path) || directUrls.Any(url => path.StartsWith(url)))
            //    {
            //        AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken, new CookieOptions()
            //        {
            //            HttpOnly = false,
            //            Secure = false,
            //            IsEssential = true,
            //            SameSite = SameSiteMode.Strict,
            //        });
            //    }

            //    return nextDelegate(context);
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("forumCategory", "f/{name:minlength(3)}", new { controller = "Categories", action = "ByName" });

                endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute("Error", "{*url}", new { controller = "Home", action = "HandleError" });

                endpoints.MapRazorPages();
            });
        }
    }
}
