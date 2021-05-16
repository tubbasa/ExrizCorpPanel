using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayMenuItem
    {
        public int Id { get; set; }
        public string Menu { get; set; }
        public string Title { get; set; }
        public string Keys { get; set; }
        public string Url { get; set; }

        public string UrlId { get; set; }
        public int? RawNumber { get; set; }
        public bool? IsIndexable { get; set; }
        public string RelatedMenu { get; set; }

        public int? RelatedMenuId { get; set; }
        public int? LangId { get; set; }

        public string Language { get; set; }

    }
}
