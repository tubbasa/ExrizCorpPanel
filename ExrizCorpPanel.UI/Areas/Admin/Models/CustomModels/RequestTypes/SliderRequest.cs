using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class SliderRequest
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public int? ImageId { get; set; }
        public int? RawNumber { get; set; }
        public int? LangId { get; set; }
        public int GalleryId { get; set; }
        public IFormFile image { get; set; }
        public int? Url { get; set; }
    }
}
