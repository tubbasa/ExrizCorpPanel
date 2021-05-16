using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class MultiPageCategoriesModel
    {
        public List<PageModel> pages { get; set; }
        public List<PageModel> categories { get; set; }
        public string CategoryName { get; set; }
    }
}
