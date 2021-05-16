using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class AreaDetail
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }
        public int? LangId { get; set; }
        public string IconName { get; set; }

        public PageArea Area { get; set; }
        public Image Image { get; set; }
    }
}
