using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class SliderGalleryRepository : ISliderGalleryRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<SliderGallery> GetAll()
        {
            return _context.SliderGallery;
        }

        public SliderGallery GetById(int id)
        {
            return _context.SliderGallery.FirstOrDefault(x => x.Id == id);
        }

        public SliderGallery Get(Expression<Func<SliderGallery, bool>> expression)
        {
            return _context.SliderGallery.FirstOrDefault(expression);
        }

        public IQueryable<SliderGallery> GetMany(Expression<Func<SliderGallery, bool>> expression)
        {
            return _context.SliderGallery.Where(expression);
        }

        public void Insert(SliderGallery obj)
        {
            _context.SliderGallery.Add(obj);
        }

        public int InsertAndGetId(SliderGallery gallery)
        {
            _context.SliderGallery.Add(gallery);
             _context.SaveChanges();
            var id = gallery.Id;
            return id;
        }

        public void Update(SliderGallery obj)
        {
            _context.SliderGallery.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.SliderGallery.Remove(selected);
            }
        }

        public int count()
        {
            return _context.SliderGallery.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
