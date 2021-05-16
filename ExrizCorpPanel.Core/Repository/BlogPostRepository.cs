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
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<BlogPost> GetAll()
        {
            return _context.BlogPost.Include(x=>x.Blog).Include(x=>x.Lang);
        }

       public int InsertAndgetId(BlogPost post)
        {
            _context.BlogPost.Add(post);
            _context.SaveChanges();
            var id = post.Id;
            return id;
        }
        public BlogPost GetById(int id)
        {
            return _context.BlogPost.FirstOrDefault(x => x.Id == id);
        }

        public BlogPost Get(Expression<Func<BlogPost, bool>> expression)
        {
            return _context.BlogPost.FirstOrDefault(expression);
        }

        public IQueryable<BlogPost> GetMany(Expression<Func<BlogPost, bool>> expression)
        {
            return _context.BlogPost.Where(expression);
        }

        public void Insert(BlogPost obj)
        {
            _context.BlogPost.Add(obj);
        }

        public void Update(BlogPost obj)
        {
            _context.BlogPost.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.BlogPost.Remove(selected);
            }
        }

        public int count()
        {
            return _context.BlogPost.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<BlogPost> takeLast(int count)
        {
            IEnumerable<BlogPost> result = _context.BlogPost.OrderByDescending(x => x.Date).Take(count);
            return result;
        }
    }
}
