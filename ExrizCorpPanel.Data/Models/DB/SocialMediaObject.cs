using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class SocialMediaObject
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string Smprofile { get; set; }
        public string ProfileName { get; set; }

        public SocialMediaTypes Type { get; set; }
    }
}
