using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class MenuItems
    {
        public MenuItems()
        {
            InverseRelatedMenuItem = new HashSet<MenuItems>();
        }

        public int Id { get; set; }
        public int? MenuId { get; set; }
        public string Title { get; set; }
        public string Keys { get; set; }
        public int UrlId { get; set; }
        public int? RawNumber { get; set; }
        public bool? IsIndexable { get; set; }
        public int? LangId { get; set; }
        public int? RelatedMenuItemId { get; set; }

        public Menu Menu { get; set; }
        public MenuItems RelatedMenuItem { get; set; }
        public ICollection<MenuItems> InverseRelatedMenuItem { get; set; }
    }
}
