using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class CommentDetail
    {
        public int Id { get; set; }
        public int? CustomerCommentId { get; set; }
        public int? LangId { get; set; }
        public string CustomerComment { get; set; }

        public CustomerComments CustomerCommentNavigation { get; set; }
        public Language Lang { get; set; }
    }
}
