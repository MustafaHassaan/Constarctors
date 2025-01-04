using Dataaccess.Models;
using Repo.UOW.Unitofworkservices;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class Projectviewmodel
    {
        private readonly Unitofwork _IUW;
        private Project Pro;
        List<Projectdata> Prodata;
        public Projectviewmodel()
        {
            _IUW = new Unitofwork();
            Prodata = new List<Projectdata>();
        }
        public List<Projectdata> LoadProjectcmb()
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var result = Proj
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList();
            var results = new List<Projectdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Projectdata
                    {
                        ID = date.ID,
                        Projectname = date.Projectname,
                        Amount = date.Amount,
                        Amountvat = date.Amountvat,
                        Partnername = date.Partners.Partnername,
                        Note = date.Note,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Transactiondata> LoadProjectrptdt(string DTF, string DTT, string Prtname, List<Searchbyprojectname> SPN)
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            var results = new List<Transactiondata>();
            decimal TotCreditor = 0;
            decimal TotDebitor = 0;
            decimal TotVat = 0;
            decimal TotBalance = 0;
            double TotRemining = 0;
            double Rem = 0;
            decimal Blc = 0;
            var PN = "";
            if (DTF != "" && DTT != "")
            {
                var result = Trans.Where(d => d.TDate >= Convert.ToDateTime(DTF) && d.TDate <= Convert.ToDateTime(DTT))
                                  .GroupBy(a => a.ID)
                                  .ToList();
                foreach (var a in result)
                {
                    foreach (var date in a)
                    {
                        if (date.Projects.Projectname != PN)
                        {
                            TotCreditor = 0;
                            TotDebitor = 0;
                            TotVat = 0;
                            TotBalance = 0;
                            TotRemining = 0;
                            Rem = 0;
                            Blc = 0;
                            PN = "";
                        }
                        //غير مدفوع
                        if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                        {
                            var res = date.Creditor - date.Debitor;
                            Blc += res;
                        }
                        // مدفوع
                        if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                        {
                            var res = date.Creditor + date.Debitor;
                            Blc += res;

                        }
                        Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                        var GRem = Math.Round(Rem, 2);
                        TotCreditor += date.Creditor;
                        TotDebitor += date.Debitor;
                        TotVat += date.Vatamount;
                        TotBalance += Blc;
                        TotRemining += Rem;
                        Transactiondata returnType = new Transactiondata();
                        returnType.ID = date.ID;
                        returnType.Projectname = date.Projects.Projectname;
                        PN = returnType.Projectname;
                        returnType.Partnername = date.Projects.Partners.Partnername;
                        returnType.Creditor = date.Creditor;
                        returnType.Debitor = date.Debitor;
                        returnType.TDate = date.TDate;
                        returnType.Detailes = date.Detailes;
                        returnType.Vatamount = date.Vatamount;
                        returnType.Balance = Blc;
                        returnType.Remining = GRem;
                        returnType.TotCreditor = TotCreditor;
                        returnType.TotDebitor = TotDebitor;
                        returnType.TotVat = TotVat;
                        returnType.TotBalance = returnType.Balance;
                        returnType.TotRemining = returnType.Remining;
                        results.Add(returnType);
                    }
                }
            }
            else if (Prtname != "")
            {
                var result = Trans.Where(d => d.Projects.Partners.Partnername == Prtname)
                     .GroupBy(a => a.ID)
                     .ToList();
                foreach (var a in result)
                {
                    foreach (var date in a)
                    {
                        if (date.Projects.Projectname != PN)
                        {
                            TotCreditor = 0;
                            TotDebitor = 0;
                            TotVat = 0;
                            TotBalance = 0;
                            TotRemining = 0;
                            Rem = 0;
                            Blc = 0;
                            PN = "";
                        }
                        //غير مدفوع
                        if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                        {
                            var res = date.Creditor - date.Debitor;
                            Blc += res;
                        }
                        // مدفوع
                        if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                        {
                            var res = date.Creditor + date.Debitor;
                            Blc += res;

                        }
                        Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                        var GRem = Math.Round(Rem, 2);
                        TotCreditor += date.Creditor;
                        TotDebitor += date.Debitor;
                        TotVat += date.Vatamount;
                        TotBalance += Blc;
                        TotRemining += Rem;
                        Transactiondata returnType = new Transactiondata();
                        returnType.ID = date.ID;
                        returnType.Projectname = date.Projects.Projectname;
                        PN = returnType.Projectname;
                        returnType.Partnername = date.Projects.Partners.Partnername;
                        returnType.Creditor = date.Creditor;
                        returnType.Debitor = date.Debitor;
                        returnType.TDate = date.TDate;
                        returnType.Detailes = date.Detailes;
                        returnType.Vatamount = date.Vatamount;
                        returnType.Balance = Blc;
                        returnType.Remining = GRem;
                        returnType.TotCreditor = TotCreditor;
                        returnType.TotDebitor = TotDebitor;
                        returnType.TotVat = TotVat;
                        returnType.TotBalance = returnType.Balance;
                        returnType.TotRemining = returnType.Remining;
                        results.Add(returnType);
                    }
                }
            }
            else if (SPN.Count > 0)
            {
                foreach (var itemname in SPN)
                {
                    var result = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname))
     .GroupBy(a => a.ID)
     .ToList();
                    foreach (var a in result)
                    {
                        foreach (var date in a)
                        {
                            if (date.Projects.Projectname != PN)
                            {
                                TotCreditor = 0;
                                TotDebitor = 0;
                                TotVat = 0;
                                TotBalance = 0;
                                TotRemining = 0;
                                Rem = 0;
                                Blc = 0;
                                PN = "";
                            }
                            //غير مدفوع
                            if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                            {
                                var res = date.Creditor - date.Debitor;
                                Blc += res;
                            }
                            // مدفوع
                            if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                            {
                                var res = date.Creditor + date.Debitor;
                                Blc += res;

                            }
                            Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                            var GRem = Math.Round(Rem, 2);
                            TotCreditor += date.Creditor;
                            TotDebitor += date.Debitor;
                            TotVat += date.Vatamount;
                            TotBalance += Blc;
                            TotRemining += Rem;
                            Transactiondata returnType = new Transactiondata();
                            returnType.ID = date.ID;
                            returnType.Projectname = date.Projects.Projectname;
                            PN = returnType.Projectname;
                            returnType.Partnername = date.Projects.Partners.Partnername;
                            returnType.Creditor = date.Creditor;
                            returnType.Debitor = date.Debitor;
                            returnType.TDate = date.TDate;
                            returnType.Detailes = date.Detailes;
                            returnType.Vatamount = date.Vatamount;
                            returnType.Balance = Blc;
                            returnType.Remining = GRem;
                            returnType.TotCreditor = TotCreditor;
                            returnType.TotDebitor = TotDebitor;
                            returnType.TotVat = TotVat;
                            returnType.TotBalance = returnType.Balance;
                            returnType.TotRemining = returnType.Remining;
                            results.Add(returnType);
                        }
                    }
                }
            }
            else if (SPN.Count > 0 && DTF != "" && DTT != "")
            {
                foreach (var itemname in SPN)
                {
                    var result = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname) && b.TDate >= Convert.ToDateTime(DTF) && b.TDate <= Convert.ToDateTime(DTT))
     .GroupBy(a => a.ID)
     .ToList();
                    foreach (var a in result)
                    {
                        foreach (var date in a)
                        {
                            if (date.Projects.Projectname != PN)
                            {
                                TotCreditor = 0;
                                TotDebitor = 0;
                                TotVat = 0;
                                TotBalance = 0;
                                TotRemining = 0;
                                Rem = 0;
                                Blc = 0;
                                PN = "";
                            }
                            //غير مدفوع
                            if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                            {
                                var res = date.Creditor - date.Debitor;
                                Blc += res;
                            }
                            // مدفوع
                            if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                            {
                                var res = date.Creditor + date.Debitor;
                                Blc += res;

                            }
                            Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                            var GRem = Math.Round(Rem, 2);
                            TotCreditor += date.Creditor;
                            TotDebitor += date.Debitor;
                            TotVat += date.Vatamount;
                            TotBalance += Blc;
                            TotRemining += Rem;
                            Transactiondata returnType = new Transactiondata();
                            returnType.ID = date.ID;
                            returnType.Projectname = date.Projects.Projectname;
                            PN = returnType.Projectname;
                            returnType.Partnername = date.Projects.Partners.Partnername;
                            returnType.Creditor = date.Creditor;
                            returnType.Debitor = date.Debitor;
                            returnType.TDate = date.TDate;
                            returnType.Detailes = date.Detailes;
                            returnType.Vatamount = date.Vatamount;
                            returnType.Balance = Blc;
                            returnType.Remining = GRem;
                            returnType.TotCreditor = TotCreditor;
                            returnType.TotDebitor = TotDebitor;
                            returnType.TotVat = TotVat;
                            returnType.TotBalance = returnType.Balance;
                            returnType.TotRemining = returnType.Remining;
                            results.Add(returnType);
                        }
                    }
                }
            }
            else if (Prtname != "" && DTF != "" && DTT != "")
            {
                var result = Trans.Where(b => b.Projects.Partners.Partnername == Prtname && b.TDate >= Convert.ToDateTime(DTF) && b.TDate <= Convert.ToDateTime(DTT))
.GroupBy(a => a.ID)
.ToList();
                foreach (var a in result)
                {
                    foreach (var date in a)
                    {
                        if (date.Projects.Projectname != PN)
                        {
                            TotCreditor = 0;
                            TotDebitor = 0;
                            TotVat = 0;
                            TotBalance = 0;
                            TotRemining = 0;
                            Rem = 0;
                            Blc = 0;
                            PN = "";
                        }
                        //غير مدفوع
                        if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                        {
                            var res = date.Creditor - date.Debitor;
                            Blc += res;
                        }
                        // مدفوع
                        if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                        {
                            var res = date.Creditor + date.Debitor;
                            Blc += res;

                        }
                        Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                        var GRem = Math.Round(Rem, 2);
                        TotCreditor += date.Creditor;
                        TotDebitor += date.Debitor;
                        TotVat += date.Vatamount;
                        TotBalance += Blc;
                        TotRemining += Rem;
                        Transactiondata returnType = new Transactiondata();
                        returnType.ID = date.ID;
                        returnType.Projectname = date.Projects.Projectname;
                        PN = returnType.Projectname;
                        returnType.Partnername = date.Projects.Partners.Partnername;
                        returnType.Creditor = date.Creditor;
                        returnType.Debitor = date.Debitor;
                        returnType.TDate = date.TDate;
                        returnType.Detailes = date.Detailes;
                        returnType.Vatamount = date.Vatamount;
                        returnType.Balance = Blc;
                        returnType.Remining = GRem;
                        returnType.TotCreditor = TotCreditor;
                        returnType.TotDebitor = TotDebitor;
                        returnType.TotVat = TotVat;
                        returnType.TotBalance = returnType.Balance;
                        returnType.TotRemining = returnType.Remining;
                        results.Add(returnType);
                    }
                }
            }
            else if (SPN.Count > 0 && Prtname != "")
            {
                foreach (var itemname in SPN)
                {
                    var result = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname) && b.Projects.Partners.Partnername == Prtname)
     .GroupBy(a => a.ID)
     .ToList();
                    foreach (var a in result)
                    {
                        foreach (var date in a)
                        {
                            if (date.Projects.Projectname != PN)
                            {
                                TotCreditor = 0;
                                TotDebitor = 0;
                                TotVat = 0;
                                TotBalance = 0;
                                TotRemining = 0;
                                Rem = 0;
                                Blc = 0;
                                PN = "";
                            }
                            //غير مدفوع
                            if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                            {
                                var res = date.Creditor - date.Debitor;
                                Blc += res;
                            }
                            // مدفوع
                            if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                            {
                                var res = date.Creditor + date.Debitor;
                                Blc += res;

                            }
                            Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                            var GRem = Math.Round(Rem, 2);
                            TotCreditor += date.Creditor;
                            TotDebitor += date.Debitor;
                            TotVat += date.Vatamount;
                            TotBalance += Blc;
                            TotRemining += Rem;
                            Transactiondata returnType = new Transactiondata();
                            returnType.ID = date.ID;
                            returnType.Projectname = date.Projects.Projectname;
                            PN = returnType.Projectname;
                            returnType.Partnername = date.Projects.Partners.Partnername;
                            returnType.Creditor = date.Creditor;
                            returnType.Debitor = date.Debitor;
                            returnType.TDate = date.TDate;
                            returnType.Detailes = date.Detailes;
                            returnType.Vatamount = date.Vatamount;
                            returnType.Balance = Blc;
                            returnType.Remining = GRem;
                            returnType.TotCreditor = TotCreditor;
                            returnType.TotDebitor = TotDebitor;
                            returnType.TotVat = TotVat;
                            returnType.TotBalance = returnType.Balance;
                            returnType.TotRemining = returnType.Remining;
                            results.Add(returnType);
                        }
                    }
                }
            }
            else
            {
                var result = Trans.GroupBy(a => a.ID).ToList();
                foreach (var a in result)
                {
                    foreach (var date in a)
                    {
                        if (date.Projects.Projectname != PN)
                        {
                            TotCreditor = 0;
                            TotDebitor = 0;
                            TotVat = 0;
                            TotBalance = 0;
                            TotRemining = 0;
                            Rem = 0;
                            Blc = 0;
                            PN = "";
                        }
                        //غير مدفوع
                        if (date.Creditor <= date.Debitor && date.Creditor == 0 && date.Debitor != 0)
                        {
                            var res = date.Creditor - date.Debitor;
                            Blc += res;
                        }
                        // مدفوع
                        if (date.Creditor >= date.Debitor && date.Creditor > 0 && date.Debitor == 0)
                        {
                            var res = date.Creditor + date.Debitor;
                            Blc += res;

                        }
                        Rem += (Convert.ToDouble(date.Creditor) / 1.15) - Convert.ToDouble(date.Debitor - date.Vatamount);
                        var GRem = Math.Round(Rem, 2);
                        TotCreditor += date.Creditor;
                        TotDebitor += date.Debitor;
                        TotVat += date.Vatamount;
                        TotBalance += Blc;
                        TotRemining += Rem;
                        Transactiondata returnType = new Transactiondata();
                        returnType.ID = date.ID;
                        returnType.Projectname = date.Projects.Projectname;
                        PN = returnType.Projectname;
                        returnType.Partnername = date.Projects.Partners.Partnername;
                        returnType.Creditor = date.Creditor;
                        returnType.Debitor = date.Debitor;
                        returnType.TDate = date.TDate;
                        returnType.Detailes = date.Detailes;
                        returnType.Vatamount = date.Vatamount;
                        returnType.Balance = Blc;
                        returnType.Remining = GRem;
                        returnType.TotCreditor = TotCreditor;
                        returnType.TotDebitor = TotDebitor;
                        returnType.TotVat = TotVat;
                        returnType.TotBalance = returnType.Balance;
                        returnType.TotRemining = returnType.Remining;
                        results.Add(returnType);
                    }
                }
            }       
            return results;
        }
        private void Loopdata() { 
        }
        public List<Projectdata> LoadProjectrpt(string DTF, string DTT, string Prtname, List<Searchbyprojectname> SPN)
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var Trans = _IUW.TblTransactions.GetAll();
            List<Projectdata> res = new List<Projectdata>();
            if (DTF != "" && DTT != "")
            {
                var summ = Trans.Where(a => a.TDate >= Convert.ToDateTime(DTF) && a.TDate <= Convert.ToDateTime(DTT))
    .GroupBy(t => t.Proid)
.Select(t => new
{
    Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
    Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
    Date = t.Select(d => d.TDate).LastOrDefault(),
    dec1Sum = t.Sum(d => d.Creditor),
    dec2Sum = t.Sum(d => d.Debitor),
    Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

}).ToList();
                foreach (var item in summ)
                {
                    res.Add(new Projectdata()
                    {
                        Partnername = item.Prtname.ToString(),
                        Projectname = item.Proname.ToString(),
                        Date = item.Date.ToString(),
                        Creditor = item.dec1Sum,
                        Debitor = item.dec2Sum,
                        Amount = item.Balance,
                    });
                }
            }
            else if (Prtname != "")
            {
                var summ = Trans.Where(a => a.Projects.Partners.Partnername == Prtname)
.GroupBy(t => t.Proid)
.Select(t => new
{
Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
Date = t.Select(d => d.TDate).LastOrDefault(),
dec1Sum = t.Sum(d => d.Creditor),
dec2Sum = t.Sum(d => d.Debitor),
Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

}).ToList();
                foreach (var item in summ)
                {
                    res.Add(new Projectdata()
                    {
                        Partnername = item.Prtname.ToString(),
                        Projectname = item.Proname.ToString(),
                        Date = item.Date.ToString(),
                        Creditor = item.dec1Sum,
                        Debitor = item.dec2Sum,
                        Amount = item.Balance,
                    });
                }
            }
            else if (SPN.Count > 0)
            {
                foreach (var itemname in SPN)
                {
                    var summ = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname))
                                    .GroupBy(t => t.Proid)
                                    .Select(t => new
                                    {
                                    Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
                                    Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
                                    Date = t.Select(d => d.TDate).LastOrDefault(),
                                    dec1Sum = t.Sum(d => d.Creditor),
                                    dec2Sum = t.Sum(d => d.Debitor),
                                    Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

                                    }).ToList();
                    foreach (var item in summ)
                    {
                        res.Add(new Projectdata()
                        {
                            Partnername = item.Prtname.ToString(),
                            Projectname = item.Proname.ToString(),
                            Date = item.Date.ToString(),
                            Creditor = item.dec1Sum,
                            Debitor = item.dec2Sum,
                            Amount = item.Balance,
                        });
                    }
                }
            }
            else if (SPN.Count > 0 && DTF != "" && DTT != "")
            {
                foreach (var itemname in SPN)
                {
                    var summ = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname) && b.TDate >= Convert.ToDateTime(DTF) && b.TDate <= Convert.ToDateTime(DTT))
                                    .GroupBy(t => t.Proid)
                                    .Select(t => new
                                    {
                                        Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
                                        Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
                                        Date = t.Select(d => d.TDate).LastOrDefault(),
                                        dec1Sum = t.Sum(d => d.Creditor),
                                        dec2Sum = t.Sum(d => d.Debitor),
                                        Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

                                    }).ToList();
                    foreach (var item in summ)
                    {
                        res.Add(new Projectdata()
                        {
                            Partnername = item.Prtname.ToString(),
                            Projectname = item.Proname.ToString(),
                            Date = item.Date.ToString(),
                            Creditor = item.dec1Sum,
                            Debitor = item.dec2Sum,
                            Amount = item.Balance,
                        });
                    }
                }
            }
            else if (Prtname != "" && DTF != "" && DTT != "")
            {
                var summ = Trans.Where(a => a.Projects.Partners.Partnername == Prtname && a.TDate >= Convert.ToDateTime(DTF) && a.TDate <= Convert.ToDateTime(DTT))
.GroupBy(t => t.Proid)
.Select(t => new
{
    Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
    Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
    Date = t.Select(d => d.TDate).LastOrDefault(),
    dec1Sum = t.Sum(d => d.Creditor),
    dec2Sum = t.Sum(d => d.Debitor),
    Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

}).ToList();
                foreach (var item in summ)
                {
                    res.Add(new Projectdata()
                    {
                        Partnername = item.Prtname.ToString(),
                        Projectname = item.Proname.ToString(),
                        Date = item.Date.ToString(),
                        Creditor = item.dec1Sum,
                        Debitor = item.dec2Sum,
                        Amount = item.Balance,
                    });
                }
            }
            else if (SPN.Count > 0 && Prtname != "")
            {
                foreach (var itemname in SPN)
                {
                    var summ = Trans.Where(b => Trans.All(P => P.Projects.Projectname == itemname.Projectname) && b.Projects.Partners.Partnername == Prtname)
                                    .GroupBy(t => t.Proid)
                                    .Select(t => new
                                    {
                                        Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
                                        Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
                                        Date = t.Select(d => d.TDate).LastOrDefault(),
                                        dec1Sum = t.Sum(d => d.Creditor),
                                        dec2Sum = t.Sum(d => d.Debitor),
                                        Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),

                                    }).ToList();
                    foreach (var item in summ)
                    {
                        res.Add(new Projectdata()
                        {
                            Partnername = item.Prtname.ToString(),
                            Projectname = item.Proname.ToString(),
                            Date = item.Date.ToString(),
                            Creditor = item.dec1Sum,
                            Debitor = item.dec2Sum,
                            Amount = item.Balance,
                        });
                    }
                }
            }
            else
            {
                var summ = Trans.GroupBy(t => t.Proid)
                                .Select(t => new
                                {
                                    Prtname = t.Select(d => d.Projects.Partners.Partnername).LastOrDefault(),
                                    Proname = t.Select(d => d.Projects.Projectname).LastOrDefault(),
                                    Date = t.Select(d => d.TDate).LastOrDefault(),
                                    dec1Sum = t.Sum(d => d.Creditor),
                                    dec2Sum = t.Sum(d => d.Debitor),
                                    Balance = t.Sum(d => d.Creditor) - t.Sum(d => d.Debitor),
                                    VAT = t.Sum(d => d.Vatamount),
                                }).ToList();
                foreach (var item in summ)
                {
                    res.Add(new Projectdata()
                    {
                        Partnername = item.Prtname.ToString(),
                        Projectname = item.Proname.ToString(),
                        Date = item.Date.ToString(),
                        Creditor = item.dec1Sum,
                        Debitor = item.dec2Sum,
                        Amount = item.Balance,
                        Amountvat = item.VAT,
                    });
                }
            }
            return res;
        }
        public List<Projectdata> LoadProjects()
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var result = Proj
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList().Take(20);
            var results = new List<Projectdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Projectdata
                    {
                        ID = date.ID,
                        Projectname = date.Projectname,
                        Amount = date.Amount,
                        Amountvat = date.Amountvat,
                        Partnername = date.Partners.Partnername,
                        Note = date.Note,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Projectdata> LoadProjectcombo1()
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var result = Proj
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList();
            var results = new List<Projectdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Projectdata
                    {
                        ID = date.ID,
                        Projectname = date.Projectname,
                        Amount = date.Amount,
                        Amountvat = date.Amountvat,
                        Partnername = date.Partners.Partnername,
                        Note = date.Note,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public List<Projectdata> LoadProjectcombo2()
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            var result = Proj
            .GroupBy(a => a.ID)
            .OrderByDescending(a => a.Min(n => n.ID))
            .ToList();
            var results = new List<Projectdata>();
            foreach (var a in result)
            {
                foreach (var date in a)
                {
                    var returnType = new Projectdata
                    {
                        ID = date.ID,
                        Projectname = date.Projectname,
                        Amount = date.Amount,
                        Amountvat = date.Amountvat,
                        Partnername = date.Partners.Partnername,
                        Note = date.Note,
                    };

                    results.Add(returnType);
                }
            }
            return results;
        }
        public bool AddProjects(Project _Pro)
        {
            try
            {
                Pro = _Pro;
                _IUW.Projects.Add(_Pro);
                _IUW.Complete();
                return true;
            }
            
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool EditProjects(Project _Pro)
        {
            try
            {
                Pro = _Pro;
                _IUW.Projects.Update(_Pro);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteProjects(int id)
        {
            try
            {
                _IUW.Projects.Delbyid(id);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        IEnumerable<Projectdata> Project;
        public List<Projectdata> GetPartnerName(string Proname, decimal Amount, string Partner)
        {
            var Part = _IUW.Partners.GetAll();
            var Proj = _IUW.Projects.GetAll();
            Prodata = new List<Projectdata>();
            if (!string.IsNullOrEmpty(Proname) && Amount != 0 && !string.IsNullOrEmpty(Partner))
            {
                var searchResults = Proj.Where(p => p.Projectname.Contains(Proname) && 
                                               p.Amount == Amount &&
                                               p.Partners.Partnername == Partner);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (!string.IsNullOrEmpty(Proname) && Amount != 0)
            {
                var searchResults = Proj.Where(p => p.Projectname.Contains(Proname) &&
                                               p.Amount == Amount);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (!string.IsNullOrEmpty(Proname) && !string.IsNullOrEmpty(Partner))
            {
                var searchResults = Proj.Where(p => p.Projectname.Contains(Proname) &&
                                               p.Partners.Partnername == Partner);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (Amount != 0 && !string.IsNullOrEmpty(Partner))
            {
                var searchResults = Proj.Where(P => P.Amount == Amount &&
                                               P.Partners.Partnername == Partner);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (!string.IsNullOrEmpty(Proname))
            {
                var searchResults = Proj.Where(p => p.Projectname.Contains(Proname));
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (Amount != 0)
            {
                var searchResults = Proj.Where(p => p.Amount == Amount);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else if (!string.IsNullOrEmpty(Partner))
            {
                var searchResults = Proj.Where(p => p.Partners.Partnername == Partner);
                foreach (var item in searchResults)
                {
                    Prodata.Add(new Projectdata
                    {
                        ID = item.ID,
                        Projectname = item.Projectname,
                        Amount = item.Amount,
                        Amountvat = item.Amountvat,
                        Partnername = item.Partners.Partnername,
                        Note = item.Note,
                    });
                }
            }
            else
            {
                LoadProjects();
            }
            var Listdata = Prodata.OrderByDescending(p => p.ID).Take(20);
            return Listdata.ToList();
        }
        public int GetAddedproject()
        {
            int Projectid = 0;
            var Datalist = _IUW.Projects.GetAll();
            Projectid = Datalist.Select(i => i.ID).Last();
            return Projectid;
        }
        public int GetAddedTransaction()
        {
            int Projectid = 0;
            var Datalist = _IUW.TblTransactions.GetAll();
            Projectid = Datalist.Select(i => i.ID).Last();
            return Projectid;
        }
    }
}
