using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayCategoryLanguageVariables
    {
        public int Id { get; set; }
        public int? LangId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Tags { get; set; }
        public string CategoryDesc { get; set; }
    }
}
