using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class SocialTypeRequest
    {
        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public IFormFile MediaIcon { get; set; }
        public Image MediaIconImage { get; set; }
    }
}
