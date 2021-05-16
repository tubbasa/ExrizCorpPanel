using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class SiteDetail
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteTags { get; set; }
        public string SiteDesc { get; set; }
        public string TelNo { get; set; }
        public string GsmNo { get; set; }
        public int? ThemeId { get; set; }
        public string SiteTitle { get; set; }
        public string SiteTagLine { get; set; }

        public Themes Theme { get; set; }
    }
}
