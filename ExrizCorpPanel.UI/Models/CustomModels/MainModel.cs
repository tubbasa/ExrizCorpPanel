using ExrizCorpPanel.UI.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.CustomModels
{
    public class MainModel
    {
        public List<ExrizCorpPanel.UI.Models.Components.Slider> sliders { get; set; }
        public List<ExrizCorpPanel.UI.Models.Components.Banner> banners { get; set; }
        public List<Job> jobs { get; set; }
        public List<JobLanguage> jobresources{ get; set; }
        public List<Reference> references{ get; set; }
        public List<ExrizCorpPanel.UI.Models.Components.Resources> stringResources { get; set; }
        public List<AreaDetail> areas { get; set; }
        public List<PageModel> pages { get; set; }
        public List<BlogPostModel> blogPosts { get; set; }




    }
}
