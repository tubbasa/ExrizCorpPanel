using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExrizCorpPanel.Core.Repository
{
    public class SliderRepository:ISliderRepository
    {
        private readonly ExrizKurumsalContext _context = new ExrizKurumsalContext();
        public IEnumerable<Slider> GetAll()
        {
            return _context.Slider;
        }

        public Slider GetById(int id)
        {
            return _context.Slider.FirstOrDefault(x => x.Id == id);
        }

        public Slider Get(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.FirstOrDefault(expression);
        }

        public IQueryable<Slider> GetMany(Expression<Func<Slider, bool>> expression)
        {
            return _context.Slider.Where(expression);
        }

        public void Insert(Slider obj)
        {
            _context.Slider.Add(obj);
        }

        public int InsertAndGetId(Slider slider)
        {
            _context.Slider.Add(slider);
            _context.SaveChanges();
            var id = slider.Id;
            return id;
        }

        public void Update(Slider obj)
        {
            _context.Slider.Update(obj);
        }

        public void Delete(int id)
        {
            var selected = GetById(id);
            if (selected != null)
            {
                _context.Slider.Remove(selected);
            }
        }

        public int count()
        {
            return _context.Slider.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
