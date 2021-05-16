using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class JobLanguageRequest
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Content { get; set; }
        public string JobName { get; set; }

        public int LangId { get; set; }

        public IList<Image> images { get; set; }
        public IList<IFormFile> imagestoUpload { get; set; }


    }
}
