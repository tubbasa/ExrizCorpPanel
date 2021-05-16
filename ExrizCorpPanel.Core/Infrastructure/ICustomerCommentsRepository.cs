using System;
using System.Collections.Generic;
using System.Text;
using ExrizCorpPanel.Data;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public interface ICustomerCommentsRepository:IRepository<CustomerComments>
    {
        int InsertAndgetId(CustomerComments comment);
    }
}
