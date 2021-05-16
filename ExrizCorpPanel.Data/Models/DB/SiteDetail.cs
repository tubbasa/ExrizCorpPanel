using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
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
        public string SiteAddress { get; set; }
        public string AnalyticsApikey { get; set; }
        public int? MenuBranches { get; set; }
        public int? LogoImageId { get; set; }
        public string SiteEmail { get; set; }
        public int? FaviconImageId { get; set; }
        public int? SmallLogoImageId { get; set; }

        public Image FaviconImage { get; set; }
        public Image LogoImage { get; set; }
        public Themes Theme { get; set; }
    }
}
