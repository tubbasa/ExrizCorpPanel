using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayCategory
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        
        public List<DisplayCategoryLanguageVariables> LanguageVariables { get; set; }
    }
}
