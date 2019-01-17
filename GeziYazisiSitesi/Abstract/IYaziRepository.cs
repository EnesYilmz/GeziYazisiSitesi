using GeziYazisiSitesi.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeziYazisiSitesi.Abstract
{
    public interface IYaziRepository
    {
        Yazi GetById(int yaziId);
        IQueryable<Yazi> GetAll();
        void AddYazi(Yazi entity);
        void UpdateYazi(Yazi entity);
        void DeleteYazi(int yaziId);
    }
}
