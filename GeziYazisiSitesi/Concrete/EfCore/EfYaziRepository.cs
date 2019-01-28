using GeziYazisiSitesi.Abstract;
using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Concrete.EfCore
{
    public class EfYaziRepository : IYaziRepository
    {
        private GeziContext context;

        public EfYaziRepository(GeziContext _context)
        {
            context = _context;
        }
        public void AddYazi(Yazi entity)
        {
            context.Yazis.Add(entity);
            context.SaveChanges();
        }

        public void DeleteYazi(int yaziId)
        {
            var yazi = context.Yazis.FirstOrDefault(p => p.YaziId == yaziId);
            if(yazi != null)
            {
                context.Yazis.Remove(yazi);
                context.SaveChanges();
            }
        }

        public IQueryable<Yazi> GetAll()
        {
            return context.Yazis;
        }

        public Yazi GetById(int yaziId)
        {
            return context.Yazis.FirstOrDefault(p => p.YaziId == yaziId);
        }

        public void UpdateYazi(Yazi entity)
        {
            var yazi = GetById(entity.YaziId);

            if(yazi != null)
            {
                yazi.Baslik = entity.Baslik;
                yazi.Icerik = entity.Icerik;
                yazi.Resim = entity.Resim;
                context.SaveChanges();
            }
        }
    }
}
