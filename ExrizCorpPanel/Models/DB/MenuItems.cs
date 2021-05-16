using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class MenuItems
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }
        public string Keys { get; set; }
        public string Url { get; set; }

        public Menu Menu { get; set; }
    }
}
