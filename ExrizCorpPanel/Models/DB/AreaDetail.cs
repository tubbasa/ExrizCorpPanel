using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class AreaDetail
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }

        public OnePageArea Area { get; set; }
        public Image Image { get; set; }
    }
}
