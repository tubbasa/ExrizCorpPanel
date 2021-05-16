using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class SocialMediaTypes
    {
        public SocialMediaTypes()
        {
            SocialMediaObject = new HashSet<SocialMediaObject>();
        }

        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public int SocialMediaImageId { get; set; }

        public ICollection<SocialMediaObject> SocialMediaObject { get; set; }
    }
}
