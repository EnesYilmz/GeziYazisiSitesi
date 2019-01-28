using GeziYazisiSitesi.Abstract;
using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Concrete.EfCore
{
    public class EfUlkeRepository : IUlkeRepository
    {
        private GeziContext context;

        public EfUlkeRepository(GeziContext _context)
        {
            context = _context;
        }
        public void AddUlke(Ulke entity)
        {
            context.Ulkes.Add(entity);
            context.SaveChanges();
        }

        public void DeleteUlke(int ulkeId)
        {
            var yazi = context.Ulkes.FirstOrDefault(p => p.UlkeId == ulkeId);
            if(yazi != null)
            {
                context.Ulkes.Remove(yazi);
                context.SaveChanges();
            }
        }

        public IQueryable<Ulke> GetAll()
        {
            return context.Ulkes;
        }

        public Ulke GetById(int ulkeId)
        {
            return context.Ulkes.FirstOrDefault(p => p.UlkeId == ulkeId);
        }

        public void UpdateUlke(Ulke entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
