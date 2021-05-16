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
    public class LanguageRepository:ILanguageRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Language> GetAll()
        {
            return _context.Language;
        }

        public Language GetById(int id)
        {
            return _context.Language.FirstOrDefault(x => x.Id == id);
        }

        public Language Get(Expression<Func<Language, bool>> expression)
        {
            return _context.Language.FirstOrDefault(expression);
        }

        public IQueryable<Language> GetMany(Expression<Func<Language, bool>> expression)
        {
            return _context.Language.Where(expression);
        }

        public void Insert(Language obj)
        {
            _context.Language.Add(obj);
        }
        public int InsertAndGetId(Language obj)
        {
            _context.Language.Add(obj);
            _context.SaveChanges();
            var id = obj.Id;
            return id;
        }

        public void Update(Language obj)
        {
            _context.Language.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Language.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Language.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
