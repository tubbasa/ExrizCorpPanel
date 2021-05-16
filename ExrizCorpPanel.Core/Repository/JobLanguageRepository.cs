using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class JobLanguageRepository:IJobLanguageRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();

        public IEnumerable<JobLanguageMapping> GetAll()
        {
            return _context.JobLanguageMapping;
        }

        public JobLanguageMapping GetById(int id)
        {
            return _context.JobLanguageMapping.FirstOrDefault(x => x.Id == id);
        }

        public JobLanguageMapping Get(Expression<Func<JobLanguageMapping, bool>> expression)
        {
            return _context.JobLanguageMapping.FirstOrDefault(expression);
        }

        public IQueryable<JobLanguageMapping> GetMany(Expression<Func<JobLanguageMapping, bool>> expression)
        {
            return _context.JobLanguageMapping.Where(expression);
        }

        public void Insert(JobLanguageMapping obj)
        {
            _context.JobLanguageMapping.Add(obj);
        }

        public int InsertAndGetId(JobLanguageMapping job)
        {
            _context.JobLanguageMapping.Add(job);
            _context.SaveChanges();
            var id = job.Id;
            return id;
        }
        public void Update(JobLanguageMapping obj)
        {
            _context.JobLanguageMapping.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.JobLanguageMapping.Remove(selected);
            }
        }

        public int count()
        {
            return _context.JobLanguageMapping.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
