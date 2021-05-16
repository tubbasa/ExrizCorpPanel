using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class BannerRequest
    {

        public int Id { get; set; }
        public string BannerName { get; set; }
        public string BannerLink { get; set; }
        public string BannerContent { get; set; }
        public int? Image { get; set; }
        public int? LangId { get; set; }

        public string imageUrl { get; set; }
        public List<IFormFile> banner { get; set; }
    }
}
