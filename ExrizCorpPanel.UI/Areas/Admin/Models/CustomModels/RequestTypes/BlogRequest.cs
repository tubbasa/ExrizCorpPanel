using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class BlogRequest
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public List<SelectListItem> CategoryList { get; set; }
    }
}
