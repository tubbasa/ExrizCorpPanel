using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class ReferenceJobRepository:IReferenceJobRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();

        public IEnumerable<ReferenceJobMapping> GetAll()
        {
            return _context.ReferenceJobMapping.Include(x=>x.Reference);
        }

        public ReferenceJobMapping GetById(int id)
        {
            return _context.ReferenceJobMapping.FirstOrDefault(x => x.Id == id);
        }

        public ReferenceJobMapping Get(Expression<Func<ReferenceJobMapping, bool>> expression)
        {
            return _context.ReferenceJobMapping.FirstOrDefault(expression);
        }

        public IQueryable<ReferenceJobMapping> GetMany(Expression<Func<ReferenceJobMapping, bool>> expression)
        {
            return _context.ReferenceJobMapping.Where(expression).Include(x=>x.Reference).Include(x=>x.JobLanguageMapping);
        }

        public void Insert(ReferenceJobMapping obj)
        {
            _context.ReferenceJobMapping.Add(obj);
        }

        public int InsertAndGetId(ReferenceJobMapping job)
        {
            _context.ReferenceJobMapping.Add(job);
            _context.SaveChanges();
            var id = job.Id;
            return id;
        }
        public void Update(ReferenceJobMapping obj)
        {
            _context.ReferenceJobMapping.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.ReferenceJobMapping.Remove(selected);
            }
        }

        public int count()
        {
            return _context.ReferenceJobMapping.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
