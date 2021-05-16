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
   public class BannerRepository:IBannerRepository
    {

        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Banner> GetAll()
        {
            return _context.Banner.Include(x=>x.Lang);
        }

        public Banner GetById(int id)
        {
            return _context.Banner.FirstOrDefault(x => x.Id == id);
        }

        public Banner Get(Expression<Func<Banner, bool>> expression)
        {
            return _context.Banner.FirstOrDefault(expression);
        }

        public IQueryable<Banner> GetMany(Expression<Func<Banner, bool>> expression)
        {
            return _context.Banner.Where(expression);
        }

        public int InsertAndgetId(Banner banner)
        {
            _context.Banner.Add(banner);
            _context.SaveChanges();
            var id = banner.Id;
            return id;
        }

        public void Update(Banner obj)
        {
            _context.Banner.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Banner.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Banner.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        void IRepository<Banner>.Insert(Banner obj)
        {
            throw new NotImplementedException();
        }
    }
}
