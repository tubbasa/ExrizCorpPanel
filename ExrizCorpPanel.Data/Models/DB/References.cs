using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class References
    {
        public References()
        {
            ReferenceJobMapping = new HashSet<ReferenceJobMapping>();
        }

        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string Url { get; set; }
        public int? ReferenceImage { get; set; }

        public Image ReferenceImageNavigation { get; set; }
        public ICollection<ReferenceJobMapping> ReferenceJobMapping { get; set; }
    }
}
