using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class SiteDetailRequest
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
        public string SiteBigLogoStr { get; set; }
        public string SiteEmail { get; set; }
        public int? FaviconImageId { get; set; }
        public IFormFile SiteBigLogo { get; set; }
        public IFormFile SiteSmallLogo { get; set; }
        public int? SiteSmallLogoId { get; set; }
        public string SiteSmallLogoStr { get; set; }
    }
}
