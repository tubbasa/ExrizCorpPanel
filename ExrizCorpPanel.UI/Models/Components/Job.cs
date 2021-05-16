using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class Job
    {
        public int Id { get; set; }
        public int? ReferenceId { get; set; }
        public string ReferenceName { get; set; }

        public string JobSystemName { get; set; }
    }
}
