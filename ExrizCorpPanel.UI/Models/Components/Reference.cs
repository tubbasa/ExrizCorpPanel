using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Reference
    {
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string Url { get; set; }
        public int? ReferenceImage { get; set; }

        public string ReferenceImageUrl { get; set; }
    }
}
