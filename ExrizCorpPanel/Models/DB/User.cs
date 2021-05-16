using System;
using System.Collections.Generic;

namespace ExrizCorpPanel.Models.DB
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public string Email { get; set; }
    }
}
