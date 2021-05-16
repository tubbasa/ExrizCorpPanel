using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class GalleryImageUpdateRequest
    {

        public int GalleryId { get; set; }
        public List<ImageRequest> PictureList{ get; set; }
    }
}
