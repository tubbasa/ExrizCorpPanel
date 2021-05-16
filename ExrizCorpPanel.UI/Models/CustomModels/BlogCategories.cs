using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class BlogCategories
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; }
        public int? RelatedCategoryId { get; set; }
        public int CountOfPost { get; set; }
        public string Slug { get; set; }
    }
}
