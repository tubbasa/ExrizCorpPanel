using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class PageDetail
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public int? ImageId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string SeoKeywords { get; set; }
        public int? LangId { get; set; }

        public Image Image { get; set; }
        public Language Lang { get; set; }
        public Page Page { get; set; }
    }
}
