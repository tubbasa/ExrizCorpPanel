using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class ExrizKurumsalContext : DbContext
    {
        public virtual DbSet<AreaDetail> AreaDetail { get; set; }
        public virtual DbSet<AreaType> AreaType { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryLanguageMapping> CategoryLanguageMapping { get; set; }
        public virtual DbSet<CommentDetail> CommentDetail { get; set; }
        public virtual DbSet<CustomerComments> CustomerComments { get; set; }
        public virtual DbSet<FooterObjects> FooterObjects { get; set; }
        public virtual DbSet<FooterType> FooterType { get; set; }
        public virtual DbSet<Galery> Galery { get; set; }
        public virtual DbSet<GalleryLanguageMapping> GalleryLanguageMapping { get; set; }
        public virtual DbSet<GlobalUrls> GlobalUrls { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<JobLanguageMapping> JobLanguageMapping { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageArea> PageArea { get; set; }
        public virtual DbSet<PageDetail> PageDetail { get; set; }
        public virtual DbSet<PageTypes> PageTypes { get; set; }
        public virtual DbSet<ReferenceJobMapping> ReferenceJobMapping { get; set; }
        public virtual DbSet<References> References { get; set; }
        public virtual DbSet<ResourceVariable> ResourceVariable { get; set; }
        public virtual DbSet<SiteDetail> SiteDetail { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<SliderGallery> SliderGallery { get; set; }
        public virtual DbSet<SocialMediaObject> SocialMediaObject { get; set; }
        public virtual DbSet<SocialMediaTypes> SocialMediaTypes { get; set; }
        public virtual DbSet<StringResource> StringResource { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<User> User { get; set; }

        // Unable to generate entity type for table 'dbo.ServiceLanguageMapping'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.MisionsForMainPage'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.BlogComment'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ExrizKurumsal;User id=sa;Password=1234;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaDetail>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.IconName).HasMaxLength(250);

                entity.Property(e => e.Title).HasColumnType("text");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.AreaDetail)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_AreaDetail_OnePageArea");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.AreaDetail)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_AreaDetail_Image");
            });

            modelBuilder.Entity<AreaType>(entity =>
            {
                entity.Property(e => e.AreaSystemTypeName).HasMaxLength(250);
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_Banner_Language");

                entity.HasOne(d => d.ViewingPageType)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.ViewingPageTypeId)
                    .HasConstraintName("FK_Banner_PageTypes");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(150);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Blog)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Blog_Blog");
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IsIndexable).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogPost)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_BlogPost_Blog");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.BlogPost)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_BlogPost_Image");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.BlogPost)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_BlogPost_Language");
            });

            modelBuilder.Entity<CategoryLanguageMapping>(entity =>
            {
                entity.Property(e => e.CategoryDesc).HasColumnType("text");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryLanguageMapping)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_CategoryLanguageMapping_Category");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.CategoryLanguageMapping)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_CategoryLanguageMapping_Language");
            });

            modelBuilder.Entity<CommentDetail>(entity =>
            {
                entity.HasOne(d => d.CustomerCommentNavigation)
                    .WithMany(p => p.CommentDetail)
                    .HasForeignKey(d => d.CustomerCommentId)
                    .HasConstraintName("FK_CommentDetail_CustomerComments");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.CommentDetail)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_CommentDetail_Language");
            });

            modelBuilder.Entity<FooterObjects>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.ObjectName).HasMaxLength(150);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FooterObjects)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_FooterObjects_FooterType");
            });

            modelBuilder.Entity<FooterType>(entity =>
            {
                entity.Property(e => e.TypeName).HasMaxLength(250);
            });

            modelBuilder.Entity<GalleryLanguageMapping>(entity =>
            {
                entity.Property(e => e.GalleryContent).HasColumnType("text");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryLanguageMapping)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_GalleryLanguageMapping_Galery");

                entity.HasOne(d => d.LangıdNavigation)
                    .WithMany(p => p.GalleryLanguageMapping)
                    .HasForeignKey(d => d.Langıd)
                    .HasConstraintName("FK_GalleryLanguageMapping_Language");
            });

            modelBuilder.Entity<GlobalUrls>(entity =>
            {
                entity.Property(e => e.ControllerName).HasMaxLength(150);

                entity.Property(e => e.EntityName).HasMaxLength(150);

                entity.Property(e => e.SlugName).HasMaxLength(150);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_Image_Galery");
            });

            modelBuilder.Entity<JobLanguageMapping>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobLanguageMapping)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLanguageMapping_JobLanguageMapping");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.JobLanguageMapping)
                    .HasForeignKey(d => d.LangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobLanguageMapping_Language");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LangSymbol).HasMaxLength(50);

                entity.HasOne(d => d.LangFlagIconNavigation)
                    .WithMany(p => p.Language)
                    .HasForeignKey(d => d.LangFlagIcon)
                    .HasConstraintName("FK_Language_Image");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MenuItems>(entity =>
            {
                entity.Property(e => e.IsIndexable).HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_MenuItems_Menu");

                entity.HasOne(d => d.RelatedMenuItem)
                    .WithMany(p => p.InverseRelatedMenuItem)
                    .HasForeignKey(d => d.RelatedMenuItemId)
                    .HasConstraintName("FK_MenuItems_MenuItems");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.IsIndexable).HasDefaultValueSql("((1))");

                entity.Property(e => e.PageName).HasMaxLength(150);
            });

            modelBuilder.Entity<PageArea>(entity =>
            {
                entity.Property(e => e.AreaName).HasMaxLength(150);
            });

            modelBuilder.Entity<PageDetail>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.SeoKeywords).HasColumnType("text");

                entity.Property(e => e.SubTitle).HasColumnType("text");

                entity.Property(e => e.Title).HasColumnType("text");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.PageDetail)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_PageDetail_Image");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.PageDetail)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_PageDetail_Language");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageDetail)
                    .HasForeignKey(d => d.PageId)
                    .HasConstraintName("FK_PageDetail_Page");
            });

            modelBuilder.Entity<PageTypes>(entity =>
            {
                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PageTypeSystemName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ReferenceJobMapping>(entity =>
            {
                entity.Property(e => e.JobSystemName).HasMaxLength(150);

                entity.HasOne(d => d.Reference)
                    .WithMany(p => p.ReferenceJobMapping)
                    .HasForeignKey(d => d.ReferenceId)
                    .HasConstraintName("FK_ReferenceJobMapping_References");
            });

            modelBuilder.Entity<References>(entity =>
            {
                entity.HasOne(d => d.ReferenceImageNavigation)
                    .WithMany(p => p.References)
                    .HasForeignKey(d => d.ReferenceImage)
                    .HasConstraintName("FK_References_Image");
            });

            modelBuilder.Entity<ResourceVariable>(entity =>
            {
                entity.Property(e => e.ResourceContent).HasColumnType("text");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.ResourceVariable)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_ResourceVariable_Language");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceVariable)
                    .HasForeignKey(d => d.ResourceId)
                    .HasConstraintName("FK_ResourceVariable_StringResource");
            });

            modelBuilder.Entity<SiteDetail>(entity =>
            {
                entity.Property(e => e.GsmNo).HasColumnType("nchar(10)");

                entity.Property(e => e.SiteName).HasMaxLength(50);

                entity.Property(e => e.TelNo).HasColumnType("nchar(10)");

                entity.HasOne(d => d.FaviconImage)
                    .WithMany(p => p.SiteDetailFaviconImage)
                    .HasForeignKey(d => d.FaviconImageId)
                    .HasConstraintName("FK_SiteDetail_Image1");

                entity.HasOne(d => d.LogoImage)
                    .WithMany(p => p.SiteDetailLogoImage)
                    .HasForeignKey(d => d.LogoImageId)
                    .HasConstraintName("FK_SiteDetail_Image");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SiteDetail)
                    .HasForeignKey(d => d.ThemeId)
                    .HasConstraintName("FK_SiteDetail_Themes");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.Property(e => e.SliderContent).HasMaxLength(150);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Slider)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Slider_Image");

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.Slider)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_Slider_Language");
            });

            modelBuilder.Entity<SliderGallery>(entity =>
            {
                entity.Property(e => e.SliderGalleryName).HasMaxLength(150);
            });

            modelBuilder.Entity<SocialMediaObject>(entity =>
            {
                entity.Property(e => e.Smprofile)
                    .HasColumnName("SMProfile")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SocialMediaObject)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_SocialMediaObject_SocialMediaObject");
            });

            modelBuilder.Entity<SocialMediaTypes>(entity =>
            {
                entity.Property(e => e.SocialMediaName).HasMaxLength(250);
            });

            modelBuilder.Entity<StringResource>(entity =>
            {
                entity.Property(e => e.ResourceName).HasColumnType("text");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.SurName).HasMaxLength(150);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });
        }
    }
}
