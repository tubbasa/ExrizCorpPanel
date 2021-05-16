using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Image
    {
        public Image()
        {
            AreaDetail = new HashSet<AreaDetail>();
        }

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageTag { get; set; }
        public string ImageAltTag { get; set; }
        public string ImageTitle { get; set; }

        public ICollection<AreaDetail> AreaDetail { get; set; }
    }
}
