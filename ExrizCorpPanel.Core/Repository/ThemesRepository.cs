using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class ThemesRepository:IThemesRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Themes> GetAll()
        {
            return _context.Themes;
        }

        public Themes GetById(int id)
        {
            return _context.Themes.FirstOrDefault(x => x.Id == id);
        }

        public Themes Get(Expression<Func<Themes, bool>> expression)
        {
            return _context.Themes.FirstOrDefault(expression);
        }

        public IQueryable<Themes> GetMany(Expression<Func<Themes, bool>> expression)
        {
            return _context.Themes.Where(expression);
        }

        public void Insert(Themes obj)
        {
            _context.Themes.Add(obj);

        }

        public void Update(Themes obj)
        {
            _context.Themes.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Themes.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Themes.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
