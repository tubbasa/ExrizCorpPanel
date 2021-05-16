using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class JobRequest
    {
        public int Id { get; set; }

        public IList<FormFile> images { get; set; }
        public int? ReferenceId { get; set; }

        public string JobSystemName { get; set;}
        public string Content { get; set; }
        
    }
}
