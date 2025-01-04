using Dataaccess.Models;
using Repo.UOW.Reoservices;
using Repo.UOW.Unitofworkservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class Prtnerviewmodel
    {
        private readonly Unitofwork _IUW;
        private Partner Prt;
        public Prtnerviewmodel()
        {
            _IUW = new Unitofwork();
            Prt = new Partner();
        }
        public List<Partner> LoadPartners()
        {
            var Datalist = _IUW.Partners.GetAll();
            return Datalist.ToList();
        }
        public List<Project> LoadParPro(int Prtproid)
        {
            var Datalist = _IUW.Partners.GetAll();
            var Detpro = _IUW.Projects.GetAll();
            var Detall = Detpro.Where(D => D.Prtid == Prtproid).ToList();
            return Detall.ToList();
        }
        public bool AddEditPartner(Partner _Prt)
        {
            try
            {
                Prt = _Prt;
                _IUW.Partners.Add(Prt);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeletePartner(int id)
        {
            try
            {
                _IUW.Partners.Delbyid(id);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Partner> GetPartnerName(string Prtname)
        {
            var searchResults = _IUW.Partners.Search(p => p.Partnername.Contains(Prtname));
            return searchResults.ToList();
        }
    }
}
