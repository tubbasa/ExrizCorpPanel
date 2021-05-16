using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class GalleryLanguageMapping
    {
        public int Id { get; set; }
        public int? GalleryId { get; set; }
        public int? Langıd { get; set; }
        public string GalleryTitle { get; set; }
        public string GalleryContent { get; set; }

        public Galery Gallery { get; set; }
        public Language LangıdNavigation { get; set; }
    }
}
