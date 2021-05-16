using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
namespace ExrizCorpPanel.Core.Repository
{
    public class CommentDetailRepository:ICommentDetailRepository
    {

        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<CommentDetail> GetAll()
        {
            return _context.CommentDetail;
        }

        public CommentDetail GetById(int id)
        {
            return _context.CommentDetail.FirstOrDefault(x => x.Id == id);
        }

        public CommentDetail Get(Expression<Func<CommentDetail, bool>> expression)
        {
            return _context.CommentDetail.FirstOrDefault(expression);
        }

        public IQueryable<CommentDetail> GetMany(Expression<Func<CommentDetail, bool>> expression)
        {
            return _context.CommentDetail.Where(expression);
        }

        public void Insert(CommentDetail obj)
        {
            _context.CommentDetail.Add(obj);
        }

        public void Update(CommentDetail obj)
        {
            _context.CommentDetail.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.CommentDetail.Remove(selected);
            }
        }

        public int count()
        {
            return _context.CommentDetail.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
