using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class OnePageArea
    {
        public OnePageArea()
        {
            AreaDetail = new HashSet<AreaDetail>();
            AreaQueue = new HashSet<AreaQueue>();
        }

        public int Id { get; set; }
        public string AreaName { get; set; }
        public int? LangId { get; set; }

        public Language Lang { get; set; }
        public ICollection<AreaDetail> AreaDetail { get; set; }
        public ICollection<AreaQueue> AreaQueue { get; set; }
    }
}
