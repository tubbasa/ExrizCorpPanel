using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class ReferenceJobMapping
    {
        public ReferenceJobMapping()
        {
            JobLanguageMapping = new HashSet<JobLanguageMapping>();
        }

        public int Id { get; set; }
        public int? ReferenceId { get; set; }
        public string JobSystemName { get; set; }

        public References Reference { get; set; }
        public ICollection<JobLanguageMapping> JobLanguageMapping { get; set; }
    }
}
