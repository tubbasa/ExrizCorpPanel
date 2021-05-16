using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class SiteDetailRepository:ISiteDetailRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<SiteDetail> GetAll()
        {
            return _context.SiteDetail;
        }

        public SiteDetail GetById(int id)
        {
            return _context.SiteDetail.FirstOrDefault(x => x.Id == id);
        }

        public SiteDetail Get(Expression<Func<SiteDetail, bool>> expression)
        {
            return _context.SiteDetail.FirstOrDefault(expression);
        }

        public IQueryable<SiteDetail> GetMany(Expression<Func<SiteDetail, bool>> expression)
        {
            return _context.SiteDetail.Where(expression);
        }

        public void Insert(SiteDetail obj)
        {
            _context.SiteDetail.Add(obj);
        }

        public void Update(SiteDetail obj)
        {
            _context.SiteDetail.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.SiteDetail.Remove(selected);
            }
        }

        public int count()
        {
            return _context.SiteDetail.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
