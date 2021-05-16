using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;

namespace ExrizCorpPanel.Core.Repository
{
    public class AreaDetailRepository: IAreaDetailRepository
    {
        private  readonly ExrizKurumsalContext _context= new  ExrizKurumsalContext();
        public IEnumerable<AreaDetail> GetAll()
        {
            return _context.AreaDetail;
        }

        public AreaDetail GetById(int id)
        {
            return _context.AreaDetail.FirstOrDefault(x => x.Id == id);
        }

        public AreaDetail Get(Expression<Func<AreaDetail, bool>> expression)
        {
            return _context.AreaDetail.FirstOrDefault(expression);
        }

        public IQueryable<AreaDetail> GetMany(Expression<Func<AreaDetail, bool>> expression)
        {
            return _context.AreaDetail.Where(expression);
        }

        public void Insert(AreaDetail obj)
        {
            _context.AreaDetail.Add(obj);
        }
        public int InsertAndGetId(AreaDetail obj)
        {
            _context.AreaDetail.Add(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public void Update(AreaDetail obj)
        {
            _context.AreaDetail.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected!=null)
            {
                _context.AreaDetail.Remove(selected);
            }
        }

        public int count()
        {
            return _context.AreaDetail.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
