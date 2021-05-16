using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public int? ImageId { get; set; }
        public int? RawNumber { get; set; }
        public int? LangId { get; set; }
        public int? Url { get; set; }
        public int? GalleryId { get; set; }
        public string SliderContent { get; set; }

        public Image Image { get; set; }
        public Language Lang { get; set; }
    }
}
