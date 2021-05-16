using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Page
    {
        public Page()
        {
            PageDetail = new HashSet<PageDetail>();
        }

        public int Id { get; set; }
        public string PageName { get; set; }
        public bool? IsIndexable { get; set; }
        public int? TypeId { get; set; }

        public ICollection<PageDetail> PageDetail { get; set; }
    }
}
