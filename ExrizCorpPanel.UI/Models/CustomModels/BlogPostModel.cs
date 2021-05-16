using ExrizCorpPanel.UI.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class BlogPostModel
    {
        public int BlogId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string BlogName { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        public bool? IsIndexable { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int? LangId { get; set; }
        public List<StringResource> resources { get; set; }
        public List<BlogCategories> blogCategories { get; set; }
        public List<RecentPost> recentPosts{ get; set; }
        public string Url { get; set; }
        
    }
}
