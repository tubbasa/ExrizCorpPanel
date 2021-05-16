using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Galery
    {
        public Galery()
        {
            GalleryLanguageMapping = new HashSet<GalleryLanguageMapping>();
            Image = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string GaleryName { get; set; }
        public string GaleryLink { get; set; }

        public ICollection<GalleryLanguageMapping> GalleryLanguageMapping { get; set; }
        public ICollection<Image> Image { get; set; }
    }
}
