using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class BlogPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public string Tags { get; set; }
    }
}
