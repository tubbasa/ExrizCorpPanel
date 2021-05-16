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
    public class CategoryLanguageMappingRepository:ICategoryLanguageMappingRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<CategoryLanguageMapping> GetAll()
        {
            return _context.CategoryLanguageMapping;
        }

        public CategoryLanguageMapping GetById(int id)
        {
            return _context.CategoryLanguageMapping.FirstOrDefault(x => x.Id == id);
        }

        public CategoryLanguageMapping Get(Expression<Func<CategoryLanguageMapping, bool>> expression)
        {
            return _context.CategoryLanguageMapping.FirstOrDefault(expression);
        }

        public IQueryable<CategoryLanguageMapping> GetMany(Expression<Func<CategoryLanguageMapping, bool>> expression)
        {
            return _context.CategoryLanguageMapping.Where(expression);
        }

        public void Insert(CategoryLanguageMapping obj)
        {
          
            _context.CategoryLanguageMapping.Add(obj);
            _context.SaveChanges();
        }

        public void Update(CategoryLanguageMapping obj)
        {
            _context.CategoryLanguageMapping.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.CategoryLanguageMapping.Remove(selected);
            }
        }

        public int count()
        {
            return _context.CategoryLanguageMapping.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
