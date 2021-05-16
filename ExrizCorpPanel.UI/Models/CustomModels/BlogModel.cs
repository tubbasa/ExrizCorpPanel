using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class BlogModel
    {
        public List<BlogPostModel> blogPost{ get; set; }
        public List<BlogCategories> blogCategories { get; set; }
        public List<BlogPostModel> recentPosts { get; set; }
    }
}
