using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class PageTypes
    {
        public PageTypes()
        {
            Banner = new HashSet<Banner>();
        }

        public int Id { get; set; }
        public string PageTypeSystemName { get; set; }
        public string PageName { get; set; }

        public ICollection<Banner> Banner { get; set; }
    }
}
