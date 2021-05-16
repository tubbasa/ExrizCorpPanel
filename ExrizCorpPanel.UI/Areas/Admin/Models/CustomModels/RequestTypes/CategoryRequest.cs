using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        public string RelatedCategoryId { get; set; }


    }
}
