using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class PageTypeRepository : IPageTypeRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
    

        public int count()
        {
            return _context.PageTypes.Count();
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.PageTypes.Remove(selected);
            }
        }

        public PageTypes Get(Expression<Func<PageTypes, bool>> expression)
        {
            return _context.PageTypes.FirstOrDefault(expression);
        }

        public IEnumerable<PageTypes> GetAll()
        {
            var list = _context.PageTypes.ToList();
            return list;
        }

        public PageTypes GetById(int id)
        {
            return _context.PageTypes.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<PageTypes> GetMany(Expression<Func<PageTypes, bool>> expression)
        {
            return _context.PageTypes.Where(expression);
        }

        public void Insert(PageTypes obj)
        {
            _context.PageTypes.Add(obj);
        }

        public int InsertAndGetId(PageTypes page)
        {
            _context.PageTypes.Add(page);
            _context.SaveChanges();
            var id = page.Id;
            return id;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PageTypes obj)
        {
            _context.PageTypes.Update(obj);
        }
    }
}
