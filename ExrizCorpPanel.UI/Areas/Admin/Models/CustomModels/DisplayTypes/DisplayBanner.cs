using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayBanner
    {
        public int Id { get; set; }
        public string BannerName { get; set; }
        public string BannerLink { get; set; }
        public string BannerContent { get; set; }
        public int? Image { get; set; }
        public int? LangId { get; set; }
        public string LangName { get; set; }
        public List<Language> Lang { get; set; }
    }
}
