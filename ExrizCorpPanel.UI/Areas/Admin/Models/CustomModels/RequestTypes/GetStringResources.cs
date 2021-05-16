using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class GetStringResources
    {
        public List<StringResource> resources { get; set; }
    }
}
