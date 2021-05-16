using System;
using System.Collections.Generic;
using System.Text;
using  ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public interface ILanguageRepository:IRepository<Language>
    {
        int InsertAndGetId(Language obj);
    }
}
