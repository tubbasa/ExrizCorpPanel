using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Page
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public int? LangId { get; set; }

        public Language Lang { get; set; }
    }
}
