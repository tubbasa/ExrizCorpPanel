using ExrizCorpPanel.UI.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class LayoutModel
    {

        public SiteDetail siteDetail { get; set; }

        public List<SocialMedia> socialMedia { get; set; }

        public List<Menu> menu { get; set; }
    }
}
