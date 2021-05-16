using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace ExrizCorpPanel.Core.Repository
{
    public class OnePageAreaRepository:IOnePageAreaRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<PageArea> GetAll()
        {
            return _context.PageArea.Include(x=>x.AreaDetail);
        }

        public PageArea GetById(int id)
        {
            return _context.PageArea.FirstOrDefault(x => x.Id == id);
        }

        public PageArea Get(Expression<Func<PageArea, bool>> expression)
        {
            return _context.PageArea.FirstOrDefault(expression);
        }

        public IQueryable<PageArea> GetMany(Expression<Func<PageArea, bool>> expression)
        {
            return _context.PageArea.Where(expression);
        }

        public int InsertAndGetId(PageArea obj)
        {
            _context.PageArea.Add(obj);
             _context.SaveChanges();
            var id = obj.Id;
            return id;
        }
        public void Insert(PageArea obj)
        {
            _context.PageArea.Add(obj);
        }

        public void Update(PageArea obj)
        {
            _context.PageArea.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.PageArea.Remove(selected);
            }
        }

        public int count()
        {
            return _context.PageArea.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
