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
    public class GaleryRepository:IGaleryRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Galery> GetAll()
        {
            return _context.Galery;
        }

        public Galery GetById(int id)
        {
            return _context.Galery.Include(x=>x.GalleryLanguageMapping).Include(x=>x.Image).FirstOrDefault(x => x.Id == id);
        }

        public Galery Get(Expression<Func<Galery, bool>> expression)
        {
            return _context.Galery.FirstOrDefault(expression);
        }
        public int InsertAndgetId(Galery gallery)
        {
            _context.Galery.Add(gallery);
            _context.SaveChanges();
            var id = gallery.Id;
            return id;
        }

        public IQueryable<Galery> GetMany(Expression<Func<Galery, bool>> expression)
        {
            return _context.Galery.Where(expression);
        }

        public void Insert(Galery obj)
        {
            _context.Galery.Add(obj);
        }

        public void Update(Galery obj)
        {
            _context.Galery.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Galery.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Galery.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
