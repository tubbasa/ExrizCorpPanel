using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MenuItems> MenuItems { get; set; }
    }
}
