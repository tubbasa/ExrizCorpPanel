using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Menu
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }
        public string Keys { get; set; }
        public int UrlId { get; set; }
        public int? RawNumber { get; set; }
        public bool? IsIndexable { get; set; }
        public int? LangId { get; set; }

        public string Url { get; set; }
        public int? RelatedMenuId { get; set; }
        public List<ExrizCorpPanel.UI.Models.Components.Menu> RelatedMenu { get; set; }

    }
}
