using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string Smprofile { get; set; }
        public string ProfileName { get; set; }
        public string TypeName { get; set; }
        public string TypeImage { get; set; }
    }
}
