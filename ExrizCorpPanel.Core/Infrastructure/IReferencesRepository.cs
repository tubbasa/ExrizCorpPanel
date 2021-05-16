using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public interface IReferencesRepository:IRepository<References>
    {
        int InsertAndGetId(References obj);
    }
}
