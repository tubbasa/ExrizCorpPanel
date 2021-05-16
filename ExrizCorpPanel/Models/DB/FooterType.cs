using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class FooterType
    {
        public FooterType()
        {
            FooterObjects = new HashSet<FooterObjects>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<FooterObjects> FooterObjects { get; set; }
    }
}
