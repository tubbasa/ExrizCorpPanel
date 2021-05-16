using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class SocialMediaTypes
    {
        public SocialMediaTypes()
        {
            SocialMediaObject = new HashSet<SocialMediaObject>();
        }

        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public string SocialMediaIcon { get; set; }

        public ICollection<SocialMediaObject> SocialMediaObject { get; set; }
    }
}
