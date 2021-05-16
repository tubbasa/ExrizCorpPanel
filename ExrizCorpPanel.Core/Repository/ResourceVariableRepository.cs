using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class ResourceVariableRepository:IResourceVariableRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<ResourceVariable> GetAll()
        {
            return _context.ResourceVariable;
        }

        public ResourceVariable GetById(int id)
        {
            return _context.ResourceVariable.FirstOrDefault(x => x.Id == id);
        }

        public ResourceVariable Get(Expression<Func<ResourceVariable, bool>> expression)
        {
            return _context.ResourceVariable.FirstOrDefault(expression);
        }

        public IQueryable<ResourceVariable> GetMany(Expression<Func<ResourceVariable, bool>> expression)
        {
            return _context.ResourceVariable.Where(expression);
        }

        public void Insert(ResourceVariable obj)
        {
            _context.ResourceVariable.Add(obj);
        }

        public void Update(ResourceVariable obj)
        {
            _context.ResourceVariable.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.ResourceVariable.Remove(selected);
            }
        }

        public int count()
        {
            return _context.ResourceVariable.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int InsertAndGetId(ResourceVariable obj)
        {
            _context.ResourceVariable.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }
    }
}
