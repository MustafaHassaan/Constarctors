using Dataaccess.Models;
using Repo.UOW.Unitofworkservices;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class Transactionviewmodel
    {
        private readonly Unitofwork _IUW;
        TblTransaction Trans;
        public Transactionviewmodel()
        {
            _IUW = new Unitofwork();
        }
        public List<Transactiondata> LoadTrsanactions()
        {
            List<Transactiondata> Transdata = new List<Transactiondata>();
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans
                        .GroupBy(a => a.ID)
                        .OrderByDescending(a => a.Min(n => n.ID))
                        .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public bool AddTrsanactions(TblTransaction _Trans)
        {
            try
            {
                Trans = _Trans;
                _IUW.TblTransactions.Add(_Trans);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditTrsanactions(TblTransaction _Trans)
        {
            try
            {
                Trans = _Trans;
                _IUW.TblTransactions.Update(_Trans);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteTrsanactions(int id)
        {
            try
            {
                _IUW.TblTransactions.Delbyid(id);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Project> GetPartnerName(string Proname)
        {
            var searchResults = _IUW.Projects.Search(p => p.Projectname.Contains(Proname));
            return searchResults.ToList();
        }
        public List<Transactiondata> Gettrans(string dettrans, int Pro, 
                                             decimal diptsearchfom,decimal diptsearchto, 
                                             decimal credsearchfrom,decimal credsearchto, 
                                             string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd",null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd",null);
            var result = Trans.Where(t => t.Detailes == dettrans &&
                                          t.Proid == Pro &&
                                          t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbydetailebydate(string dettrans, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Detailes == dettrans &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaileAndCred(string dettrans, decimal credsearchfrom, decimal credsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Detailes == dettrans &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaileAnddipt(string dettrans, decimal diptsearchfom, decimal diptsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Detailes == dettrans &&
                                          t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransAll(string dettrans, int pro, decimal diptsearchfom, decimal diptsearchto, decimal credsearchfrom, decimal credsearchto, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Detailes == dettrans &&
                                          t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate =     date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaileAndProbydate(string dettrans, int pro, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Detailes == dettrans &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaileAndProAndCred(string dettrans, int pro, decimal credsearchfrom, decimal credsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Detailes == dettrans &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaileAndProAnddipt(string dettrans, int pro, decimal diptsearchfom, decimal diptsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Detailes == dettrans &&
                                          t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbydetaiAndPro(string dettrans, int pro)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Detailes == dettrans)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbyProbydate(int pro, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbyProAndCred(int pro, decimal credsearchfrom, decimal credsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Creditor <= credsearchfrom &&
                                          t.Creditor >= credsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbyProanddipt(int pro, decimal diptsearchfom, decimal diptsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro &&
                                          t.Debitor <= diptsearchfom &&
                                          t.Debitor >= diptsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransGettransbydiptandcredanddate(decimal diptsearchfom, decimal diptsearchto, decimal credsearchfrom, decimal credsearchto, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbydiptandcred(decimal diptsearchfom, decimal diptsearchto, decimal credsearchfrom, decimal credsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchto &&
                                          t.Creditor >= credsearchfrom &&
                                          t.Creditor <= credsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbydipt(decimal diptsearchfom, decimal diptsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Debitor >= diptsearchfom &&
                                          t.Debitor <= diptsearchfom )
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbycredanddate(decimal credsearchfrom, decimal credsearchto, string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd", null);
            var result = Trans.Where(t => t.Creditor >= credsearchfrom && 
                                          t.Creditor <= credsearchto &&
                                          t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbycred(decimal credsearchfrom, decimal credsearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Creditor >= credsearchfrom && t.Creditor <= credsearchto)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbydate(string datesearchfrom, string datesearchto)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var DTF = DateTime.ParseExact(datesearchfrom, "yyyy-MM-dd",null);
            var DTT = DateTime.ParseExact(datesearchto, "yyyy-MM-dd",null);
            var result = Trans.Where(t => t.TDate >= DTF &&
                                          t.TDate <= DTT)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> GettransbyPro(int pro)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Proid == pro)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> Gettransbydetaile(string dettrans)
        {
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var result = Trans.Where(t => t.Detailes == dettrans)
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Transactiondata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Transactiondata
                    {
                        ID = date.ID,
                        Projectname = date.Projects.Projectname,
                        Creditor = date.Creditor,
                        Debitor = date.Debitor,
                        TDate = date.TDate,
                        Detailes = date.Detailes,
                        Vatamount = date.Vatamount,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
    }
}
