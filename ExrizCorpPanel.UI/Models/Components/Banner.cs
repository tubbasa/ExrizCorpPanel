using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Banner
    {
        public int Id { get; set; }
        public string BannerName { get; set; }
        public string BannerLink { get; set; }
        public string BannerContent { get; set; }
        public int? Image { get; set; }
        public string ImageUrl { get; set; }
        public int? LangId { get; set; }
        public int? ViewingPageTypeId { get; set; }
        public int? RowNumber { get; set; }
    }
}
