using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.DisplayTypes
{
    public class DisplayBlog
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        public string CategoryName { get; set; }
    }
}
