using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Resources
    {
        public int Id { get; set; }
        public int? ResourceId { get; set; }
        public string ResourceContent { get; set; }
        public int? LangId { get; set; }
        public string ResourceSystemName { get; set; }
    }
}
