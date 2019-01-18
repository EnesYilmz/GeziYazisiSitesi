using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Abstract
{
    public interface IUlkeRepository
    {
        Ulke GetById(int ulkeId);
        IQueryable<Ulke> GetAll();
        void AddUlke(Ulke entity);
        void UpdateUlke(Ulke entity);
        void DeleteUlke(int ulkeId);
    }
}
