using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayFooterObject
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string ObjectName { get; set; }
        public string Content { get; set; }
    }
}
