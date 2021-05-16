using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class GalleryLanguageMappingRequest
    {
        public int Id { get; set; }
        public int? GalleryId { get; set; }
        public int? Langıd { get; set; }
        public string GalleryTitle { get; set; }
        public string GalleryContent { get; set; }
    }
}
