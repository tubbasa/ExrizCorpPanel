using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaSystemTypeName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(nullable: true),
                    RelatedCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerFirm = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GaleryLink = table.Column<string>(nullable: true),
                    GaleryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalUrls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerName = table.Column<string>(maxLength: 150, nullable: true),
                    EntityId = table.Column<int>(nullable: true),
                    EntityName = table.Column<string>(maxLength: 150, nullable: true),
                    EntityTitle = table.Column<string>(nullable: true),
                    ExternalLink = table.Column<string>(nullable: true),
                    IsExternalLink = table.Column<bool>(nullable: true),
                    SlugName = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsForMob = table.Column<bool>(nullable: true),
                    IsForWeb = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsIndexable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    PageName = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaName = table.Column<string>(maxLength: 150, nullable: true),
                    RowNumber = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PageName = table.Column<string>(maxLength: 250, nullable: false),
                    PageTypeSystemName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SliderGallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    SliderGalleryName = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SocialMediaName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StringResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResourceName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowableMenuBranches = table.Column<int>(nullable: true),
                    LayoutName = table.Column<string>(nullable: true),
                    ThemeName = table.Column<string>(nullable: true),
                    ThemePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: true),
                    IsEditor = table.Column<bool>(nullable: true),
                    IsWebUser = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 250, nullable: true),
                    SurName = table.Column<string>(maxLength: 150, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Blog",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FooterObjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ObjectName = table.Column<string>(maxLength: 150, nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterObjects_FooterType",
                        column: x => x.TypeId,
                        principalTable: "FooterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GalleryId = table.Column<int>(nullable: true),
                    ImageAltTag = table.Column<string>(nullable: true),
                    ImageTag = table.Column<string>(nullable: true),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsBelongGallery = table.Column<bool>(nullable: true),
                    IsJob = table.Column<bool>(nullable: true),
                    JobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Galery",
                        column: x => x.GalleryId,
                        principalTable: "Galery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsIndexable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    Keys = table.Column<string>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    MenuId = table.Column<int>(nullable: true),
                    RawNumber = table.Column<int>(nullable: true),
                    RelatedMenuItemId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    UrlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menu",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems",
                        column: x => x.RelatedMenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaObject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileName = table.Column<string>(nullable: true),
                    SMProfile = table.Column<string>(maxLength: 250, nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMediaObject_SocialMediaObject",
                        column: x => x.TypeId,
                        principalTable: "SocialMediaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreaDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IconName = table.Column<string>(maxLength: 250, nullable: true),
                    ImageId = table.Column<int>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaDetail_OnePageArea",
                        column: x => x.AreaId,
                        principalTable: "PageArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreaDetail_Image",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LangFlagIcon = table.Column<int>(nullable: true),
                    LangName = table.Column<string>(nullable: true),
                    LangSymbol = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Language_Image",
                        column: x => x.LangFlagIcon,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceImage = table.Column<int>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Image",
                        column: x => x.ReferenceImage,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnalyticsApikey = table.Column<string>(nullable: true),
                    FaviconImageId = table.Column<int>(nullable: true),
                    GsmNo = table.Column<string>(type: "nchar(10)", nullable: true),
                    LogoImageId = table.Column<int>(nullable: true),
                    MenuBranches = table.Column<int>(nullable: true),
                    SiteAddress = table.Column<string>(nullable: true),
                    SiteDesc = table.Column<string>(nullable: true),
                    SiteEmail = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(maxLength: 50, nullable: true),
                    SiteTagLine = table.Column<string>(nullable: true),
                    SiteTags = table.Column<string>(nullable: true),
                    SiteTitle = table.Column<string>(nullable: true),
                    TelNo = table.Column<string>(type: "nchar(10)", nullable: true),
                    ThemeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteDetail_Image1",
                        column: x => x.FaviconImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteDetail_Image",
                        column: x => x.LogoImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteDetail_Themes",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BannerContent = table.Column<string>(nullable: true),
                    BannerLink = table.Column<string>(nullable: true),
                    BannerName = table.Column<string>(nullable: true),
                    Image = table.Column<int>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    RowNumber = table.Column<int>(nullable: true),
                    ViewingPageTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banner_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_PageTypes",
                        column: x => x.ViewingPageTypeId,
                        principalTable: "PageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ImageId = table.Column<int>(nullable: true),
                    IsIndexable = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    LangId = table.Column<int>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_Blog",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPost_Image",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPost_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLanguageMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryDesc = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLanguageMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLanguageMapping_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryLanguageMapping_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerComment = table.Column<string>(nullable: true),
                    CustomerCommentId = table.Column<int>(nullable: true),
                    LangId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentDetail_CustomerComments",
                        column: x => x.CustomerCommentId,
                        principalTable: "CustomerComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentDetail_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GalleryLanguageMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GalleryContent = table.Column<string>(type: "text", nullable: true),
                    GalleryId = table.Column<int>(nullable: true),
                    GalleryTitle = table.Column<string>(nullable: true),
                    Langıd = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryLanguageMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryLanguageMapping_Galery",
                        column: x => x.GalleryId,
                        principalTable: "Galery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GalleryLanguageMapping_Language",
                        column: x => x.Langıd,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ImageId = table.Column<int>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    PageId = table.Column<int>(nullable: true),
                    SeoKeywords = table.Column<string>(type: "text", nullable: true),
                    SubTitle = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageDetail_Image",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageDetail_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageDetail_Page",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourceVariable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LangId = table.Column<int>(nullable: true),
                    ResourceContent = table.Column<string>(type: "text", nullable: true),
                    ResourceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceVariable_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceVariable_StringResource",
                        column: x => x.ResourceId,
                        principalTable: "StringResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GalleryId = table.Column<int>(nullable: true),
                    ImageId = table.Column<int>(nullable: true),
                    LangId = table.Column<int>(nullable: true),
                    RawNumber = table.Column<int>(nullable: true),
                    SliderContent = table.Column<string>(maxLength: 150, nullable: true),
                    SliderName = table.Column<string>(nullable: true),
                    Url = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slider_Image",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slider_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceJobMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobSystemName = table.Column<string>(maxLength: 150, nullable: true),
                    ReferenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceJobMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceJobMapping_References",
                        column: x => x.ReferenceId,
                        principalTable: "References",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobLanguageMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    JobId = table.Column<int>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    LangId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLanguageMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLanguageMapping_JobLanguageMapping",
                        column: x => x.JobId,
                        principalTable: "ReferenceJobMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobLanguageMapping_Language",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaDetail_AreaId",
                table: "AreaDetail",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaDetail_ImageId",
                table: "AreaDetail",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_LangId",
                table: "Banner",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ViewingPageTypeId",
                table: "Banner",
                column: "ViewingPageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CategoryId",
                table: "Blog",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogId",
                table: "BlogPost",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_ImageId",
                table: "BlogPost",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_LangId",
                table: "BlogPost",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLanguageMapping_CategoryId",
                table: "CategoryLanguageMapping",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLanguageMapping_LangId",
                table: "CategoryLanguageMapping",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDetail_CustomerCommentId",
                table: "CommentDetail",
                column: "CustomerCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDetail_LangId",
                table: "CommentDetail",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_FooterObjects_TypeId",
                table: "FooterObjects",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryLanguageMapping_GalleryId",
                table: "GalleryLanguageMapping",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryLanguageMapping_Langıd",
                table: "GalleryLanguageMapping",
                column: "Langıd");

            migrationBuilder.CreateIndex(
                name: "IX_Image_GalleryId",
                table: "Image",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLanguageMapping_JobId",
                table: "JobLanguageMapping",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLanguageMapping_LangId",
                table: "JobLanguageMapping",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LangFlagIcon",
                table: "Language",
                column: "LangFlagIcon");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RelatedMenuItemId",
                table: "MenuItems",
                column: "RelatedMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PageDetail_ImageId",
                table: "PageDetail",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageDetail_LangId",
                table: "PageDetail",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_PageDetail_PageId",
                table: "PageDetail",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceJobMapping_ReferenceId",
                table: "ReferenceJobMapping",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_References_ReferenceImage",
                table: "References",
                column: "ReferenceImage");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceVariable_LangId",
                table: "ResourceVariable",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceVariable_ResourceId",
                table: "ResourceVariable",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteDetail_FaviconImageId",
                table: "SiteDetail",
                column: "FaviconImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteDetail_LogoImageId",
                table: "SiteDetail",
                column: "LogoImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteDetail_ThemeId",
                table: "SiteDetail",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ImageId",
                table: "Slider",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_LangId",
                table: "Slider",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaObject_TypeId",
                table: "SocialMediaObject",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaDetail");

            migrationBuilder.DropTable(
                name: "AreaType");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "CategoryLanguageMapping");

            migrationBuilder.DropTable(
                name: "CommentDetail");

            migrationBuilder.DropTable(
                name: "FooterObjects");

            migrationBuilder.DropTable(
                name: "GalleryLanguageMapping");

            migrationBuilder.DropTable(
                name: "GlobalUrls");

            migrationBuilder.DropTable(
                name: "JobLanguageMapping");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "PageDetail");

            migrationBuilder.DropTable(
                name: "ResourceVariable");

            migrationBuilder.DropTable(
                name: "SiteDetail");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "SliderGallery");

            migrationBuilder.DropTable(
                name: "SocialMediaObject");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PageArea");

            migrationBuilder.DropTable(
                name: "PageTypes");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "CustomerComments");

            migrationBuilder.DropTable(
                name: "FooterType");

            migrationBuilder.DropTable(
                name: "ReferenceJobMapping");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "StringResource");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "SocialMediaTypes");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Galery");
        }
    }
}
