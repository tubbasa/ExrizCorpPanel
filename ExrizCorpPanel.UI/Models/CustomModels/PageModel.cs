using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class PageModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public bool? IsIndexable { get; set; }
        public int PageDetailId { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl{ get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string SeoKeywords { get; set; }
        public int? LangId { get; set; }
        public string Url { get; set; }
        public int PageTypeId { get; set; }
        public string PageTypeName { get; set; }
    }
}
