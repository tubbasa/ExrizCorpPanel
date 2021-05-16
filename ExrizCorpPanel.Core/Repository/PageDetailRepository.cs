using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class PageDetailRepository:IPageDetailRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<PageDetail> GetAll()
        {
            return _context.PageDetail;
        }

        public PageDetail GetById(int id)
        {
            return _context.PageDetail.FirstOrDefault(x => x.Id == id);
        }

        public PageDetail Get(Expression<Func<PageDetail, bool>> expression)
        {
            return _context.PageDetail.FirstOrDefault(expression);
        }

        public IQueryable<PageDetail> GetMany(Expression<Func<PageDetail, bool>> expression)
        {
            return _context.PageDetail.Where(expression);
        }

        public void Insert(PageDetail obj)
        {
            _context.PageDetail.Add(obj);
        }

        public int InsertAndGetId(PageDetail page)
        {
            _context.Add(page);
            _context.SaveChanges();
            var id = page.Id;
            return id;
        }
        

        public void Update(PageDetail obj)
        {
            _context.PageDetail.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.PageDetail.Remove(selected);
            }
        }

        public int count()
        {
            return _context.PageDetail.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
