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
    public class FooterObjectsRepository: IFooterObjectsRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<FooterObjects> GetAll()
        {
            return _context.FooterObjects.Include(x=>x.Type);
        }

        public FooterObjects GetById(int id)
        {
            return _context.FooterObjects.FirstOrDefault(x => x.Id == id);
        }

        public FooterObjects Get(Expression<Func<FooterObjects, bool>> expression)
        {
            return _context.FooterObjects.FirstOrDefault(expression);
        }

        public IQueryable<FooterObjects> GetMany(Expression<Func<FooterObjects, bool>> expression)
        {
            return _context.FooterObjects.Where(expression);
        }

        public void Insert(FooterObjects obj)
        {
            _context.FooterObjects.Add(obj);
        }

        public void Update(FooterObjects obj)
        {
            _context.FooterObjects.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.FooterObjects.Remove(selected);
            }
        }

        public int count()
        {
            return _context.FooterObjects.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
