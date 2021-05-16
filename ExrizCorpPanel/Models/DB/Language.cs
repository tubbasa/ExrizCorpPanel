using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class Language
    {
        public Language()
        {
            CategoryLanguageMapping = new HashSet<CategoryLanguageMapping>();
            OnePageArea = new HashSet<OnePageArea>();
            Page = new HashSet<Page>();
            ResourceVariable = new HashSet<ResourceVariable>();
        }

        public int Id { get; set; }
        public string LangName { get; set; }
        public string LangSymbol { get; set; }
        public int? LangFlagIcon { get; set; }

        public ICollection<CategoryLanguageMapping> CategoryLanguageMapping { get; set; }
        public ICollection<OnePageArea> OnePageArea { get; set; }
        public ICollection<Page> Page { get; set; }
        public ICollection<ResourceVariable> ResourceVariable { get; set; }
    }
}
