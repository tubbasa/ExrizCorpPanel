using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<User> GetAll()
        {
            return _context.User;
        }

        public User GetById(int id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        public User Get(Expression<Func<User, bool>> expression)
        {
            return _context.User.FirstOrDefault(expression);
        }

        public IQueryable<User> GetMany(Expression<Func<User, bool>> expression)
        {
            return _context.User.Where(expression);
        }

        public void Insert(User obj)
        {
            _context.User.Add(obj);

        }

        public void Update(User obj)
        {
            _context.User.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.User.Remove(selected);
            }
        }

        public int count()
        {
            return _context.User.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
