using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class BlogCategoryModel
    {
        public List<BlogPostModel> blogPost { get; set; }
        public List<BlogCategories> blogCategories { get; set; }
    }
}
