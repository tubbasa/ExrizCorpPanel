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
    public class MenuItemsRepository:IMenuItemsRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<MenuItems> GetAll()
        {
            return _context.MenuItems.Include(x => x.Menu);
        }

        public MenuItems GetById(int id)
        {
            return _context.MenuItems.FirstOrDefault(x => x.Id == id);
        }

        public MenuItems Get(Expression<Func<MenuItems, bool>> expression)
        {
            return _context.MenuItems.FirstOrDefault(expression);
        }

        public IQueryable<MenuItems> GetMany(Expression<Func<MenuItems, bool>> expression)
        {
            return _context.MenuItems.Where(expression);
        }

        public void Insert(MenuItems obj)
        {
            _context.MenuItems.Add(obj);
        }

        public void Update(MenuItems obj)
        {
            _context.MenuItems.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.MenuItems.Remove(selected);
            }
        }

        public int count()
        {
            return _context.MenuItems.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
