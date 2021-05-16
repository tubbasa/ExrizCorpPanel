using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class CategoryLanguageMapping
    {
        public int Id { get; set; }
        public int? LangId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Tags { get; set; }
        public string CategoryDesc { get; set; }

        public Category Category { get; set; }
        public Language Lang { get; set; }
    }
}
