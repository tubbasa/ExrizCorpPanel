using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Core.Repository;
using ExrizCorpPanel.UI.Areas.Admin.CustomFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using CodingBlast;

namespace ExrizCorpPanel.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAreaDetailRepository, AreaDetailRepository>();
            services.AddScoped<IAreaTypeRepository, AreaTypeRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryLanguageMappingRepository, CategoryLanguageMappingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFooterObjectsRepository, FooterObjectsRepository>();
            services.AddScoped<IFooterTypeRepository, FooterTypeRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IMenuItemsRepository, MenuItemsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IOnePageAreaRepository, OnePageAreaRepository>();
            services.AddScoped<IPageDetailRepository, PageDetailRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IResourceVariableRepository, ResourceVariableRepository>();
            services.AddScoped<ISiteDetailRepository, SiteDetailRepository>();
            services.AddScoped<ISocialMediaObjectRepository, SocialMediaObjectRepository>();
            services.AddScoped<ISocialMediaTypesRepository, SocialMediaTypesRepository>();
            services.AddScoped<IStringResourceRepository, StringResourceRepository>();
            services.AddScoped<IThemesRepository, ThemesRepository>();
            services.AddScoped<IReferencesRepository, ReferencesRepository>();
            services.AddScoped<IReferenceJobRepository, ReferenceJobRepository>();
            services.AddScoped<IJobLanguageRepository, JobLanguageRepository>();
            services.AddScoped<ICustomerCommentsRepository, CustomerCommentsRepository>();
            services.AddScoped<ICommentDetailRepository, CommentDetailRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ISliderGalleryRepository, SliderGalleryRepository>();
            services.AddScoped<ISlugRepository, SlugRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IGaleryRepository, GaleryRepository>();
            services.AddScoped<IGalleryMappingRepository, GalleryLanguageMappingRepository>();
            services.AddScoped<IPageTypeRepository, PageTypeRepository>();
            services.AddScoped<LoginFilterAttribute>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".TanvirArjel.Session";
                options.IdleTimeout = TimeSpan.FromDays(1);
            });
            //services.AddSingleton<ITagHelperComponent>(new GoogleAnalyticsTagHelperComponent("UA-29415951-2"));
        }

    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {


                routes.MapRoute(
                 name: "areas",
                 template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

           //     routes.MapRoute(
           //name: "test",
           //template: "{slug}",
           //defaults: new { controller = "Home", action = "checkGlobalUrl" });

            });

        }
    }
}
