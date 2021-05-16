using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Language
    {
        public Language()
        {
            Banner = new HashSet<Banner>();
            BlogPost = new HashSet<BlogPost>();
            CategoryLanguageMapping = new HashSet<CategoryLanguageMapping>();
            CommentDetail = new HashSet<CommentDetail>();
            GalleryLanguageMapping = new HashSet<GalleryLanguageMapping>();
            JobLanguageMapping = new HashSet<JobLanguageMapping>();
            PageDetail = new HashSet<PageDetail>();
            ResourceVariable = new HashSet<ResourceVariable>();
            Slider = new HashSet<Slider>();
        }

        public int Id { get; set; }
        public string LangName { get; set; }
        public string LangSymbol { get; set; }
        public int? LangFlagIcon { get; set; }

        public Image LangFlagIconNavigation { get; set; }
        public ICollection<Banner> Banner { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
        public ICollection<CategoryLanguageMapping> CategoryLanguageMapping { get; set; }
        public ICollection<CommentDetail> CommentDetail { get; set; }
        public ICollection<GalleryLanguageMapping> GalleryLanguageMapping { get; set; }
        public ICollection<JobLanguageMapping> JobLanguageMapping { get; set; }
        public ICollection<PageDetail> PageDetail { get; set; }
        public ICollection<ResourceVariable> ResourceVariable { get; set; }
        public ICollection<Slider> Slider { get; set; }
    }
}
