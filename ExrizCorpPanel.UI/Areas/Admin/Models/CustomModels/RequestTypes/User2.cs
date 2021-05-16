using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes
{
    public class User2
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEditor { get; set; }
        public bool IsWebUser { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsActive { get; set; }
    }
}
