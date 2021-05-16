using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class SocialMediaTypesRepository:ISocialMediaTypesRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<SocialMediaTypes> GetAll()
        {
            return _context.SocialMediaTypes;
        }

        public SocialMediaTypes GetById(int id)
        {
            return _context.SocialMediaTypes.FirstOrDefault(x => x.Id == id);
        }

        public SocialMediaTypes Get(Expression<Func<SocialMediaTypes, bool>> expression)
        {
            return _context.SocialMediaTypes.FirstOrDefault(expression);
        }

        public IQueryable<SocialMediaTypes> GetMany(Expression<Func<SocialMediaTypes, bool>> expression)
        {
            return _context.SocialMediaTypes.Where(expression);
        }

        public void Insert(SocialMediaTypes obj)
        {
            _context.SocialMediaTypes.Add(obj);

        }

        public int InsertAndGetId(SocialMediaTypes type)
        {
            _context.SocialMediaTypes.Add(type);
            _context.SaveChanges();
            var id = type.Id;
            return id;
        }
        public void Update(SocialMediaTypes obj)
        {
            _context.SocialMediaTypes.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.SocialMediaTypes.Remove(selected);
            }
        }

        public int count()
        {
            return _context.SocialMediaTypes.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
