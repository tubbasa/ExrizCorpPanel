using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class PagesModel
    {
        public PagesModel()
        {
            this.pageList = new List<PageModel>();
        }
        public List<PageModel> pageList{ get; set; }
    }
}
