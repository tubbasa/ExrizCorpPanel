using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class BlogPostRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public string Tags { get; set; }
        public bool? IsIndexable { get; set; }
        public int? BlogId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int LangId{ get; set; }

        public string ImageUrl { get; set; }
        public List<IFormFile> images { get; set; }

        public List<SelectListItem> blogs { get; set; }
    }
}
