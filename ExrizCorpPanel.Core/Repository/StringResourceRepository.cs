using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class StringResourceRepository:IStringResourceRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<StringResource> GetAll()
        {
            return _context.StringResource;
        }

        public StringResource GetById(int id)
        {
            return _context.StringResource.FirstOrDefault(x => x.Id == id);
        }

        public StringResource Get(Expression<Func<StringResource, bool>> expression)
        {
            return _context.StringResource.FirstOrDefault(expression);
        }

        public IQueryable<StringResource> GetMany(Expression<Func<StringResource, bool>> expression)
        {
            return _context.StringResource.Where(expression);
        }

        public void Insert(StringResource obj)
        {
            _context.StringResource.Add(obj);

        }

        public void Update(StringResource obj)
        {
            _context.StringResource.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.StringResource.Remove(selected);
            }
        }

        public int count()
        {
            return _context.StringResource.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int InsertAndGetId(StringResource obj)
        {
            _context.StringResource.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }
    }
}
