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
    public class CustomerCommentsRepository:ICustomerCommentsRepository
    {

        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<CustomerComments> GetAll()
        {
            return _context.CustomerComments;
        }

        public CustomerComments GetById(int id)
        {
            return _context.CustomerComments.Include(x=>x.CommentDetail).FirstOrDefault(x => x.Id == id);
        }

        public CustomerComments Get(Expression<Func<CustomerComments, bool>> expression)
        {
            return _context.CustomerComments.FirstOrDefault(expression);
        }

        public IQueryable<CustomerComments> GetMany(Expression<Func<CustomerComments, bool>> expression)
        {
            return _context.CustomerComments.Where(expression);
        }

        public void Insert(CustomerComments obj)
        {
            _context.CustomerComments.Add(obj);
        }
        public int InsertAndgetId(CustomerComments comment)
        {
            _context.CustomerComments.Add(comment);
            _context.SaveChanges();
            var id = comment.Id;
            return id;
        }
        public void Update(CustomerComments obj)
        {
            _context.CustomerComments.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.CustomerComments.Remove(selected);
            }
        }

        public int count()
        {
            return _context.CustomerComments.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
