using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class SlugRepository:ISlugRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<GlobalUrls> GetAll()
        {
            return _context.GlobalUrls;
        }

        public GlobalUrls GetById(int id)
        {
            return _context.GlobalUrls.FirstOrDefault(x => x.Id == id);
        }

        public GlobalUrls Get(Expression<Func<GlobalUrls, bool>> expression)
        {
            return _context.GlobalUrls.FirstOrDefault(expression);
        }

        public IQueryable<GlobalUrls> GetMany(Expression<Func<GlobalUrls, bool>> expression)
        {
            return _context.GlobalUrls.Where(expression);
        }

        public void Insert(GlobalUrls obj)
        {
            _context.GlobalUrls.Add(obj);
        }

        public int InsertAndGetId(GlobalUrls url)
        {
            _context.GlobalUrls.Add(url);
            _context.SaveChanges();
            var id = url.Id;
            return id;
        }

        public void Update(GlobalUrls obj)
        {
            _context.GlobalUrls.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.GlobalUrls.Remove(selected);
            }
        }

        public int count()
        {
            return _context.GlobalUrls.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
