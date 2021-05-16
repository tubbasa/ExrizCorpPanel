using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class AreaDetail
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }
        public int? LangId { get; set; }
        public string IconName { get; set; }
        public int rowNumber { get; set; }
        public string imageUrl { get; set; }
        public int typeId { get; set; }
        public string typeName{ get; set; }
    }
}
