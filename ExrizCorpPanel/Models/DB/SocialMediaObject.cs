using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class SocialMediaObject
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }

        public SocialMediaTypes Type { get; set; }
    }
}
