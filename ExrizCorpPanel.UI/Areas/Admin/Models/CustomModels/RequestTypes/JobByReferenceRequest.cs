using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class JobByReferenceRequest
    {
        public int Id { get; set; }
        public int? ReferenceId { get; set; }

        public string ReferenceName { get; set; }

        public int Count { get; set; }
    }
}
