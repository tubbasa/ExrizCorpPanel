using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Blog
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
