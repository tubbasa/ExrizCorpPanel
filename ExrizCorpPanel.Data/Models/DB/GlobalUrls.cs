using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class GlobalUrls
    {
        public int Id { get; set; }
        public int? EntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityTitle { get; set; }
        public string ControllerName { get; set; }
        public string SlugName { get; set; }
        public bool? IsExternalLink { get; set; }
        public string ExternalLink { get; set; }
    }
}
