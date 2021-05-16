using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class AreaDetailRequest
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }
        public int? LangId { get; set; }

        public string ImageUrl { get; set; }
    }
}
