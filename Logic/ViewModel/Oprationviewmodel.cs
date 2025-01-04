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
    public class Oprationviewmodel
    {
        private readonly Unitofwork _IUW;
        private Opration OPR;
        public Oprationviewmodel()
        {
            _IUW = new Unitofwork();
            OPR = new Opration();
        }
        public List<Oprationsdata> LoadOprations()
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            foreach (var item in Datalist)
            {
                if (item.Tblname == "Project")
                {
                    item.Tblname = "المشروعات";
                    var data = _IUW.Projects.Get(item.Tblid.GetValueOrDefault());
                    item.Tblid = data.ID;

                }
                if (item.Tblname == "Tbl_Transaction")
                {
                    item.Tblname = "المعاملات";
                    var data = _IUW.TblTransactions.Get(item.Tblid.GetValueOrDefault());
                    item.Tblid = data.ID;
                }
                var Pdid = PD.Select(x => x.Id == item.Id);
                if (Pdid != null)
                {
                    PD.Add(new Oprationsdata
                    {
                        Id = item.Id,
                        Detailes = item.Detailes,
                        Oprationname = item.Oprationname,
                        Tblname = item.Tblname,
                        Date = item.Date,
                        Time = item.Time,
                        Username = item.Users.Username,
                    });
                }
            }
            var Listdata = PD.OrderByDescending(p => p.Id).Take(20);
            return Listdata.ToList();
        }
        public bool AddEditOprations(Opration _OPR)
        {
            try
            {
                OPR = _OPR;
                _IUW.Oprations.Add(_OPR);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Oprationsdata> Getall(string typeentry, string entryscreen, int entryuser, 
                                          string datefrom, string dateto)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var DTF = DateTime.ParseExact(datefrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(dateto, "yyyy-MM-dd", null);
            var result = Datalist.Where(t => t.Oprationname == typeentry &&
                                             t.Tblname == entryscreen &&
                                             t.Usrid >= entryuser &&
                                             t.Date >= DTF &&
                                             t.Date <= DTT)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object GetTSU(string typeentry, string entryscreen, int entryuser)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Oprationname == typeentry &&
                                             t.Tblname == entryscreen &&
                                             t.Usrid >= entryuser)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object GetTS(string typeentry, string entryscreen)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Oprationname == typeentry &&
                                             t.Tblname == entryscreen)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Gettu(string typeentry,int entryuser)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Oprationname == typeentry &&
                                             t.Usrid >= entryuser)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Gettd(string typeentry, string datefrom, string dateto)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var DTF = DateTime.ParseExact(datefrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(dateto, "yyyy-MM-dd", null);
            var result = Datalist.Where(t => t.Oprationname == typeentry &&
                                             t.Date >= DTF &&
                                             t.Date <= DTT)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Getsu(string entryscreen, int entryuser)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Tblname == entryscreen &&
                                             t.Usrid >= entryuser)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Getsd(string entryscreen, string datefrom, string dateto)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var DTF = DateTime.ParseExact(datefrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(dateto, "yyyy-MM-dd", null);
            var result = Datalist.Where(t => t.Tblname == entryscreen &&
                                             t.Date >= DTF &&
                                             t.Date <= DTT)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Getud(int entryuser, string datefrom, string dateto)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var DTF = DateTime.ParseExact(datefrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(dateto, "yyyy-MM-dd", null);
            var result = Datalist.Where(t => t.Usrid >= entryuser &&
                                             t.Date >= DTF &&
                                             t.Date <= DTT)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object GetT(string typeentry)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Oprationname == typeentry)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object GetS(string entryscreen)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Tblname == entryscreen)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Getu(int entryuser)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var result = Datalist.Where(t => t.Usrid >= entryuser)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public object Getdate(string datefrom, string dateto)
        {
            List<Oprationsdata> PD = new List<Oprationsdata>();
            var User = _IUW.Users.GetAll();
            var Datalist = _IUW.Oprations.GetAll();
            var DTF = DateTime.ParseExact(datefrom, "yyyy-MM-dd", null);
            var DTT = DateTime.ParseExact(dateto, "yyyy-MM-dd", null);
            var result = Datalist.Where(t => t.Date >= DTF &&
                                             t.Date <= DTT)
                                .GroupBy(a => a.Id)
                                .OrderByDescending(a => a.Min(n => n.Id))
                                .ToList();
            var results = new List<Oprationsdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    if (date.Tblname == "Project")
                    {
                        date.Tblname = "المشروعات";

                    }
                    if (date.Tblname == "Tbl_Transaction")
                    {
                        date.Tblname = "المعاملات";
                    }
                    var returnType = new Oprationsdata
                    {
                        Id = date.Id,
                        Detailes = date.Detailes,
                        Oprationname = date.Oprationname,
                        Tblname = date.Tblname,
                        Date = date.Date,
                        Time = DateTime.Parse(date.Time).ToShortTimeString(),
                        Username = date.Users.Username,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
    }
}
