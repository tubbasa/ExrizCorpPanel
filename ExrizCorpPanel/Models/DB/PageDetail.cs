using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class PageDetail
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string SeoKeywords { get; set; }
    }
}
