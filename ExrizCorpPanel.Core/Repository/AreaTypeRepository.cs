using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class AreaTypeRepository:IAreaTypeRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<AreaType> GetAll()
        {
            return _context.AreaType;
        }

        public AreaType GetById(int id)
        {
            return _context.AreaType.FirstOrDefault(x => x.Id == id);
        }

        public AreaType Get(Expression<Func<AreaType, bool>> expression)
        {
            return _context.AreaType.FirstOrDefault(expression);
        }

        public IQueryable<AreaType> GetMany(Expression<Func<AreaType, bool>> expression)
        {
            return _context.AreaType.Where(expression);
        }

        public void Insert(AreaType obj)
        {
            _context.AreaType.Add(obj);
        }
        public int InsertAndGetId(AreaType obj)
        {
            _context.AreaType.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public void Update(AreaType obj)
        {
            _context.AreaType.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.AreaType.Remove(selected);
            }
        }

        public int count()
        {
            return _context.AreaType.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
