using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string BannerName { get; set; }
        public string BannerLink { get; set; }
        public string BannerContent { get; set; }
        public int? Image { get; set; }
        public int? LangId { get; set; }
        public int? ViewingPageTypeId { get; set; }
        public int? RowNumber { get; set; }

        public Language Lang { get; set; }
        public PageTypes ViewingPageType { get; set; }
    }
}
