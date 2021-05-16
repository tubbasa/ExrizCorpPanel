using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public interface IReferenceJobRepository:IRepository<ReferenceJobMapping>
    {
        int InsertAndGetId(ReferenceJobMapping job);
    }
}
