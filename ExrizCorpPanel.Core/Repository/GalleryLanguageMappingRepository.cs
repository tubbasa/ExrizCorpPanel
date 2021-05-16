using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class GalleryLanguageMappingRepository: IGalleryMappingRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<GalleryLanguageMapping> GetAll()
        {
            return _context.GalleryLanguageMapping;
        }

        public GalleryLanguageMapping GetById(int id)
        {
            return _context.GalleryLanguageMapping.FirstOrDefault(x => x.Id == id);
        }

        public GalleryLanguageMapping Get(Expression<Func<GalleryLanguageMapping, bool>> expression)
        {
            return _context.GalleryLanguageMapping.FirstOrDefault(expression);
        }

        public IQueryable<GalleryLanguageMapping> GetMany(Expression<Func<GalleryLanguageMapping, bool>> expression)
        {
            return _context.GalleryLanguageMapping.Where(expression);
        }

        public void Insert(GalleryLanguageMapping obj)
        {
            _context.GalleryLanguageMapping.Add(obj);
        }

        public void Update(GalleryLanguageMapping obj)
        {
            _context.GalleryLanguageMapping.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.GalleryLanguageMapping.Remove(selected);
            }
        }

        public int count()
        {
            return _context.GalleryLanguageMapping.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
