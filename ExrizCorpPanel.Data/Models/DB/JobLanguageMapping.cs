using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class JobLanguageMapping
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Content { get; set; }
        public string JobName { get; set; }
        public int LangId { get; set; }

        public ReferenceJobMapping Job { get; set; }
        public Language Lang { get; set; }
    }
}
