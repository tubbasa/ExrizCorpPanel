using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class ReferencesRepository:IReferencesRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<References> GetAll()
        {
            return _context.References;
        }

        public References GetById(int id)
        {
            return _context.References.Include(x=>x.ReferenceImageNavigation).FirstOrDefault(x => x.Id == id);
        }

        public References Get(Expression<Func<References, bool>> expression)
        {
            return _context.References.FirstOrDefault(expression);
        }

        public IQueryable<References> GetMany(Expression<Func<References, bool>> expression)
        {
            return _context.References.Where(expression);
        }

        public void Insert(References obj)
        {
            _context.References.Add(obj);
        }

        public int InsertAndGetId(References obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public void Update(References obj)
        {
            _context.References.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.References.Remove(selected);
            }
        }

        public int count()
        {
            return _context.References.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
