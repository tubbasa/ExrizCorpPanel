using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class PageRequest
    {
        public PageRequest()
        {
            PageDetail = new HashSet<PageDetail>();
        }

        public int Id { get; set; }
        public string PageName { get; set; }
        public string IsIndexableStr { get; set; }
        public bool IsIndexable { get; set; }
        public int SelectedType { get; set; }
        public ICollection<PageDetail> PageDetail { get; set; }
    }
}
