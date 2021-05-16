using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class BlogCategoriesModel
    {
        public List<BlogCategories> categories { get; set; }
        public List<BlogPostModel> recentPosts { get; set; }
    }
}
