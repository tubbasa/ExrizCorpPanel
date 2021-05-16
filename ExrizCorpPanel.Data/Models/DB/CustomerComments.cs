using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Data.Models.DB
{
    public partial class CustomerComments
    {
        public CustomerComments()
        {
            CommentDetail = new HashSet<CommentDetail>();
        }

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerFirm { get; set; }

        public ICollection<CommentDetail> CommentDetail { get; set; }
    }
}
