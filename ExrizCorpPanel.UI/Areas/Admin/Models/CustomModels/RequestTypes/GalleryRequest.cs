using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class GalleryRequest
    {
        public int Id { get; set; }
        public string GaleryName { get; set; }
        public string GaleryLink { get; set; }

        public IList<IFormFile> images { get; set; }
    }
}
