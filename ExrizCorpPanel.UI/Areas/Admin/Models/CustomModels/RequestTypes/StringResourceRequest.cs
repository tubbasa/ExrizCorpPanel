using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class StringResourceRequest
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public List<ResourceVariable> resources { get; set; }
        public List<Language> languages { get; set; }
    }
}
