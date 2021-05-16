using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class FooterTypeRepository:IFooterTypeRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<FooterType> GetAll()
        {
            return _context.FooterType;
        }

        public FooterType GetById(int id)
        {
            return _context.FooterType.FirstOrDefault(x => x.Id == id);
        }

        public FooterType Get(Expression<Func<FooterType, bool>> expression)
        {
            return _context.FooterType.FirstOrDefault(expression);
        }

        public IQueryable<FooterType> GetMany(Expression<Func<FooterType, bool>> expression)
        {
            return _context.FooterType.Where(expression);
        }

        public void Insert(FooterType obj)
        {
            _context.FooterType.Add(obj);
        }

        public void Update(FooterType obj)
        {
            _context.FooterType.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.FooterType.Remove(selected);
            }
        }

        public int count()
        {
            return _context.FooterType.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
