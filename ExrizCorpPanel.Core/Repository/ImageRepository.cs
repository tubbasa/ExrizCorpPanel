using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class ImageRepository:IImageRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Image> GetAll()
        {
            return _context.Image;
        }

        public Image GetById(int id)
        {
            return _context.Image.FirstOrDefault(x => x.Id == id);
        }

        public Image Get(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.FirstOrDefault(expression);
        }

        public IQueryable<Image> GetMany(Expression<Func<Image, bool>> expression)
        {
            return _context.Image.Where(expression);
        }
        public int InsertAndgetId(Image image)
        {
            _context.Image.Add(image);
            _context.SaveChanges();
            var id = image.Id;
            return id;
        }

        public void Insert(Image obj)
        {
            _context.Image.Add(obj);
        }

        public void Update(Image obj)
        {
            _context.Image.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Image.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Image.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
