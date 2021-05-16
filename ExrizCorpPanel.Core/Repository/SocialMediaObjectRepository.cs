using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class SocialMediaObjectRepository:ISocialMediaObjectRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<SocialMediaObject> GetAll()
        {
            return _context.SocialMediaObject;
        }

        public SocialMediaObject GetById(int id)
        {
            return _context.SocialMediaObject.FirstOrDefault(x => x.Id == id);
        }

        public SocialMediaObject Get(Expression<Func<SocialMediaObject, bool>> expression)
        {
            return _context.SocialMediaObject.FirstOrDefault(expression);
        }

        public IQueryable<SocialMediaObject> GetMany(Expression<Func<SocialMediaObject, bool>> expression)
        {
            return _context.SocialMediaObject.Where(expression);
        }

        public void Insert(SocialMediaObject obj)
        {
            _context.SocialMediaObject.Add(obj);
        }

        public void Update(SocialMediaObject obj)
        {
            _context.SocialMediaObject.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.SocialMediaObject.Remove(selected);
            }
        }

        public int count()
        {
            return _context.SocialMediaObject.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
