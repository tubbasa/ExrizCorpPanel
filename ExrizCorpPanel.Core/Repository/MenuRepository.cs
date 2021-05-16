using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class MenuRepository:IMenuRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Menu> GetAll()
        {
            return _context.Menu;
        }

        public Menu GetById(int id)
        {
            return _context.Menu.FirstOrDefault(x => x.Id == id);
        }

        public Menu Get(Expression<Func<Menu, bool>> expression)
        {
            return _context.Menu.FirstOrDefault(expression);
        }

        public IQueryable<Menu> GetMany(Expression<Func<Menu, bool>> expression)
        {
            return _context.Menu.Where(expression);
        }

        public void Insert(Menu obj)
        {
            _context.Menu.Add(obj);
        }

        public void Update(Menu obj)
        {
            _context.Menu.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Menu.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Menu.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
