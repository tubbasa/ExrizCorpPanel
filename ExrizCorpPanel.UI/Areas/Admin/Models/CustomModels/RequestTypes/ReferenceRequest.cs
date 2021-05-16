using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class ReferenceRequest
    {
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string Url { get; set; }
        public IFormFile ReferenceImage { get; set; }
        public int? ReferenceImageId { get; set; }
    }
}
