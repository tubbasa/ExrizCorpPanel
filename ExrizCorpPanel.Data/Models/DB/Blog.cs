using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Blog
    {
        public Blog()
        {
            BlogPost = new HashSet<BlogPost>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
    }
}
