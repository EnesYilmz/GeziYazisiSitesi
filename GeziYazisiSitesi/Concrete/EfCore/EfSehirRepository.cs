using GeziYazisiSitesi.Abstract;
using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Concrete.EfCore
{
    public class EfSehirRepository : ISehirRepository
    {
        private GeziContext context;

        public EfSehirRepository(GeziContext _context)
        {
            context = _context;
        }
        public void AddSehir(Sehir entity)
        {
            context.Sehirs.Add(entity);
            context.SaveChanges();
        }

        public void DeleteSehir(int sehirId)
        {
            var sehir = context.Sehirs.FirstOrDefault(p => p.SehirId == sehirId);
            if(sehir != null)
            {
                context.Sehirs.Remove(sehir);
                context.SaveChanges();
            }
        }

        public IQueryable<Sehir> GetAll()
        {
            return context.Sehirs;
        }

        public Sehir GetById(int sehirId)
        {
            return context.Sehirs.FirstOrDefault(p => p.SehirId == sehirId);
        }

        public void UpdateSehir(Sehir entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
