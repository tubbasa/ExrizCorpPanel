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
    public class BlogRepository:IBlogRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Blog> GetAll()
        {
            return _context.Blog.Include(x=>x.Category);
        }

        public Blog GetById(int id)
        {
            return _context.Blog.FirstOrDefault(x => x.Id == id);
        }

        public Blog Get(Expression<Func<Blog, bool>> expression)
        {
            return _context.Blog.FirstOrDefault(expression);
        }

        public IQueryable<Blog> GetMany(Expression<Func<Blog, bool>> expression)
        {
            return _context.Blog.Where(expression);
        }

        public void Insert(Blog obj)
        {
            _context.Blog.Add(obj);
        }

        public void Update(Blog obj)
        {
            _context.Blog.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Blog.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Blog.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
