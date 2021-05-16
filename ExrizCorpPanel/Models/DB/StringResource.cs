using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class StringResource
    {
        public StringResource()
        {
            ResourceVariable = new HashSet<ResourceVariable>();
        }

        public int Id { get; set; }
        public string ResourceName { get; set; }

        public ICollection<ResourceVariable> ResourceVariable { get; set; }
    }
}
