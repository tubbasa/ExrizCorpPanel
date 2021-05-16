using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
   public class PageRepository:IPageRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();

        public IEnumerable<Page> GetAll()
        {
            return _context.Page;
        }

        public Page GetById(int id)
        {
            return _context.Page.FirstOrDefault(x => x.Id == id);
        }
    

        public Page Get(Expression<Func<Page, bool>> expression)
        {
            return _context.Page.FirstOrDefault(expression);
        }

        public IQueryable<Page> GetMany(Expression<Func<Page, bool>> expression)
        {
            return _context.Page.Where(expression);
        }

        public void Insert(Page obj)
        {
            _context.Page.Add(obj);

        }

        public int InsertAndGetId(Page obj)
        {
             _context.Page.Add(obj);
            _context.SaveChanges();
            var id = obj.Id;
            return id;
        }

        public void Update(Page obj)
        {
            _context.Page.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Page.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Page.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
