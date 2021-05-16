using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class JobLanguage
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Content { get; set; }
        public string JobName { get; set; }
        public int LangId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
