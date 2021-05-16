using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Image
    {
        public Image()
        {
            AreaDetail = new HashSet<AreaDetail>();
            BlogPost = new HashSet<BlogPost>();
            Language = new HashSet<Language>();
            PageDetail = new HashSet<PageDetail>();
            References = new HashSet<References>();
            SiteDetailFaviconImage = new HashSet<SiteDetail>();
            SiteDetailLogoImage = new HashSet<SiteDetail>();
            Slider = new HashSet<Slider>();
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTag { get; set; }
        public string ImageAltTag { get; set; }
        public string ImageTitle { get; set; }
        public bool? IsBelongGallery { get; set; }
        public int? GalleryId { get; set; }
        public bool? IsJob { get; set; }
        public int? JobId { get; set; }
        public Galery Gallery { get; set; }
        public ICollection<AreaDetail> AreaDetail { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
        public ICollection<Language> Language { get; set; }
        public ICollection<PageDetail> PageDetail { get; set; }
        public ICollection<References> References { get; set; }
        public ICollection<SiteDetail> SiteDetailFaviconImage { get; set; }
        public ICollection<SiteDetail> SiteDetailLogoImage { get; set; }
        public ICollection<Slider> Slider { get; set; }
    }
}
