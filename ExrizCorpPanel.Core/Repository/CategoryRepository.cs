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
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Category> GetAll()
        {
            return _context.Category;
        }

        public Category GetById(int id)
        {
            return _context.Category.Include(x=>x.CategoryLanguageMapping).FirstOrDefault(x => x.Id == id);
        }

        public Category Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.FirstOrDefault(expression);
        }

        public IQueryable<Category> GetMany(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.Where(expression);
        }

        public void Insert(Category obj)
        {
            _context.Category.Add(obj);
        }


        public int InsertAndgetId(Category cat)
        {
            _context.Category.Add(cat);
            _context.SaveChanges();
            var id = cat.Id;
            return id;
        }
        public void Update(Category obj)
        {
            _context.Category.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Category.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Category.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
