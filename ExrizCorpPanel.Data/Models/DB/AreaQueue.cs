using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class AreaQueue
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public int? RowNumber { get; set; }

        public OnePageArea Area { get; set; }
    }
}
