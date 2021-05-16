using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class PageArea
    {
        public PageArea()
        {
            AreaDetail = new HashSet<AreaDetail>();
        }

        public int Id { get; set; }
        public string AreaName { get; set; }
        public int? RowNumber { get; set; }
        public int? TypeId { get; set; }

        public ICollection<AreaDetail> AreaDetail { get; set; }
    }
}
