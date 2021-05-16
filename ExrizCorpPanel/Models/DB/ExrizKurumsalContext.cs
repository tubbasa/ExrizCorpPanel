using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExrizCorpPanel.Models.DB
{
    public partial class ExrizKurumsalContext : DbContext
    {
        public virtual DbSet<AreaDetail> AreaDetail { get; set; }
        public virtual DbSet<AreaQueue> AreaQueue { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryLanguageMapping> CategoryLanguageMapping { get; set; }
        public virtual DbSet<FooterObjects> FooterObjects { get; set; }
        public virtual DbSet<FooterType> FooterType { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<OnePageArea> OnePageArea { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageDetail> PageDetail { get; set; }
        public virtual DbSet<ResourceVariable> ResourceVariable { get; set; }
        public virtual DbSet<SiteDetail> SiteDetail { get; set; }
        public virtual DbSet<SocialMediaObject> SocialMediaObject { get; set; }
        public virtual DbSet<SocialMediaTypes> SocialMediaTypes { get; set; }
        public virtual DbSet<StringResource> StringResource { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-L9H3O6P;Database=ExrizKurumsal;User id=sa;Password=Password1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaDetail>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

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

            modelBuilder.Entity<AreaQueue>(entity =>
            {
                entity.HasOne(d => d.Area)
                    .WithMany(p => p.AreaQueue)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_AreaQueue_OnePageArea");
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
            });

            modelBuilder.Entity<CategoryLanguageMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LangSymbol).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MenuItems>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Url).HasColumnName("URL");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_MenuItems_Menu");
            });

            modelBuilder.Entity<OnePageArea>(entity =>
            {
                entity.Property(e => e.AreaName).HasMaxLength(150);

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.OnePageArea)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_OnePageArea_Language");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.PageName).HasMaxLength(150);

                entity.HasOne(d => d.Lang)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.LangId)
                    .HasConstraintName("FK_Page_Language");
            });

            modelBuilder.Entity<PageDetail>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.SeoKeywords).HasColumnType("text");

                entity.Property(e => e.SubTitle).HasColumnType("text");

                entity.Property(e => e.Title).HasColumnType("text");
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

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.SiteDetail)
                    .HasForeignKey(d => d.ThemeId)
                    .HasConstraintName("FK_SiteDetail_Themes");
            });

            modelBuilder.Entity<SocialMediaObject>(entity =>
            {
                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SocialMediaObject)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_SocialMediaObject_SocialMediaObject");
            });

            modelBuilder.Entity<SocialMediaTypes>(entity =>
            {
                entity.Property(e => e.SocialMediaIcon).HasMaxLength(150);

                entity.Property(e => e.SocialMediaName).HasMaxLength(250);
            });

            modelBuilder.Entity<StringResource>(entity =>
            {
                entity.Property(e => e.ResourceName).HasColumnType("text");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });
        }
    }
}
