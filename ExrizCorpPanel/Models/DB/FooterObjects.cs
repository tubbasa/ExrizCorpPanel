using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class FooterObjects
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string ObjectName { get; set; }
        public string Content { get; set; }

        public FooterType Type { get; set; }
    }
}
