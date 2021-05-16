using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Slider
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int? RawNumber { get; set; }
        public int? LangId { get; set; }
        public int? Url { get; set; }
        public int? GalleryId { get; set; }
        public string SliderContent { get; set; }
    }
}
