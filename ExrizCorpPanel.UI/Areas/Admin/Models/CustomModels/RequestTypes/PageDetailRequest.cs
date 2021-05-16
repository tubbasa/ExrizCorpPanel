using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class PageDetailRequest
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string SeoKeywords { get; set; }
        public int? LangId { get; set; }
    }
}
