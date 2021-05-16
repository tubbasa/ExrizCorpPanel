using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class FooterObjectRequest
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string ObjectName { get; set; }
        public string Content { get; set; }
        public List<SelectListItem> footerTypes { get; set; }
    }
}
