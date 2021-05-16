using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Themes
    {
        public Themes()
        {
            SiteDetail = new HashSet<SiteDetail>();
        }

        public int Id { get; set; }
        public string ThemeName { get; set; }
        public string ThemePath { get; set; }

        public ICollection<SiteDetail> SiteDetail { get; set; }
    }
}
