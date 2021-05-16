using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class LanguageRequest
    {
        public int Id { get; set; }
        public string LangName { get; set; }
        public string LangSymbol { get; set; }
        public string LangFlagIcon { get; set; }
        public IList<IFormFile> icon { get; set; }
    }
}
