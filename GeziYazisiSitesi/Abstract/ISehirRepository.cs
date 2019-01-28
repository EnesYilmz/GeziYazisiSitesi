using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Abstract
{
    public interface ISehirRepository
    {
        Sehir GetById(int sehirId);
        IQueryable<Sehir> GetAll();
        void AddSehir(Sehir entity);
        void UpdateSehir(Sehir entity);
        void DeleteSehir(int sehirId);
    }
}
