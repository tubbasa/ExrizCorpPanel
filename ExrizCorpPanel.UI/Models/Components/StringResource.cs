using ExrizCorpPanel.UI.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Models.Components
{
    public class StringResource
    {
        public int Id { get; set; }
        public string ResourceName { get; set; }

        public ResourceVariables variables { get; set; }
        //hepsini göndermek yerine dil isne göre gönderilecek

    }
}
