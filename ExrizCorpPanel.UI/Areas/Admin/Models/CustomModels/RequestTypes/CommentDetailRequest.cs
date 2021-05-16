using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class CommentDetailRequest
    {
        public int Id { get; set; }
        public int? CustomerCommentId { get; set; }
        public int? LangId { get; set; }

        public string LangName { get; set; }
        public string CustomerComment { get; set; }
    }
}
