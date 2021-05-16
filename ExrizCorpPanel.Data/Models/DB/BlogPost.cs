using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class BlogPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public string Tags { get; set; }
        public bool? IsIndexable { get; set; }
        public int? BlogId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int? LangId { get; set; }
        public DateTime? Date { get; set; }

        public Blog Blog { get; set; }
        public Image Image { get; set; }
        public Language Lang { get; set; }
    }
}
