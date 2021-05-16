using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Category
    {
        public Category()
        {
            Blog = new HashSet<Blog>();
            CategoryLanguageMapping = new HashSet<CategoryLanguageMapping>();
        }

        public int Id { get; set; }
        public string FriendlyName { get; set; }
        public int? RelatedCategoryId { get; set; }

        public ICollection<Blog> Blog { get; set; }
        public ICollection<CategoryLanguageMapping> CategoryLanguageMapping { get; set; }
    }
}
