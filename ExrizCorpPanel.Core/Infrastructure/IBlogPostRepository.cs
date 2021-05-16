using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Infrastructure
{
    public  interface IBlogPostRepository:IRepository<BlogPost>
    {
        int InsertAndgetId(BlogPost post);
        IEnumerable<BlogPost> takeLast(int count);
    }
}
