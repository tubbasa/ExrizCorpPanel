using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class ResourceVariable
    {
        public int Id { get; set; }
        public int? ResourceId { get; set; }
        public string ResourceContent { get; set; }
        public int? LangId { get; set; }

        public Language Lang { get; set; }
        public StringResource Resource { get; set; }
    }
}
