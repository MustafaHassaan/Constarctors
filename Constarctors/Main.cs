using Constractors;
using CrystalDecisions.CrystalReports.Engine;
using Dataaccess.Models;
using FontAwesome.Sharp;
using Logic.ViewModel;
using MaterialSkin;
using MaterialSkin.Controls;
using Services;
using Services.Crystalandform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Constarctors
{
    public partial class Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Prtnerviewmodel _PVM;
        private Projectviewmodel _Provm;
        private Transactionviewmodel _Transvm;
        private Oprationviewmodel _OVM;
        private Userviewmodel _Usr;
        Otherconfig _OC;
        private Partner Prt;
        private Project Prj;
        private TblTransaction Trans;
        private User Usr;
        private Opration OPR;
        Messagealert MA;
        public int Staffid { get; set; }
        public string Staffusername { get; set; }
        public Main(RightToLeft RightToLeft = RightToLeft.Yes) : base(RightToLeft)
        {

            Prt = new Partner();
            Prj = new Project();
            Trans = new TblTransaction();
            Usr = new User();
            _OC = new Otherconfig();
            MA = new Messagealert();
            OPR = new Opration();
            var culture = new CultureInfo("ar-EG");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            base.RightToLeft = RightToLeft.Yes;
            InitializeComponent();
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            
            materialSkinManager.RightToLeft = RightToLeft;
            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);

            _PVM = new Prtnerviewmodel();
            _Provm = new Projectviewmodel();
            _Transvm = new Transactionviewmodel();
            _OVM = new Oprationviewmodel();
            _Usr = new Userviewmodel();
            Clearfileds();
            PrtBtnsaveanmodi.Click += PrtBtnsaveanmodi_Click;
            ProBtnSaveAndmodi.Click += ProBtnSaveAndmodi_Click;
            PrtBtndelete.Click += PrtBtndelete_Click;
            ProBtnclear.Click += ProBtnclear_Click;
            ProBtnDelet.Click += ProBtnDelet_Click;
            Btntransclear.Click += Btntransclear_Click;
            Btntransaddedit.Click += Btntransaddedit_Click;
            Btntransdel.Click += Btntransdel_Click;
            ProBtnsearch.Click += ProBtnsearch_Click;
            Btnuserclear.Click += Btnuserclear_Click;
            Btnusersave.Click += Btnusersave_Click;
            Btnuserdel.Click += Btnuserdel_Click;
            Btntranssearch.Click += Btntranssearch_Click;
            EntryBtnsearch.Click += EntryBtnsearch_Click;
            Btnrepshow.Click += Btnrepshow_Click;
            CLBCheakedall.Click += CLBCheakedall_Click;
            CLBUnCheakedall.Click += CLBUnCheakedall_Click;
        }

        private void CLBCheakedall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBProject.Items.Count; i++)
            {
                CLBProject.SetItemChecked(i, true);
            }
        }

        private void CLBUnCheakedall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLBProject.Items.Count; i++)
            {
                CLBProject.SetItemChecked(i, false);
            }
        }

        private void Btnrepshow_Click(object sender, EventArgs e)
        {
            var DTF = "";
            var DTT = "";
            var Prtname = "";
            Frmreport FR = new Frmreport();
            Dataset Dsx = new Dataset();
            ReportDocument Objrpt = new ReportDocument();
            List<Searchbyprojectname> SPN = new List<Searchbyprojectname>();
            if (Chckadaterpt.Checked)
            {
                DTF = dTPF.Value.ToString("yyyy-MM-dd");
                DTT = dTPT.Value.ToString("yyyy-MM-dd");
            }
            if (Prtsearch.Checked)
            {
                if (Partnercmb.SelectedIndex != 0)
                {
                    Prtname = Partnercmb.Text;
                }
            }
            if (Projectsearch.Checked)
            {
                if (CLBProject.CheckedItems.Count > 0)
                {
                    foreach (object itemChecked in CLBProject.CheckedItems)
                    {
                        SPN.Add(new Searchbyprojectname()
                        {
                            Projectname = itemChecked.ToString(),
                        });
                    }
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار مشروع");
                    return;
                }
            }
            if (CBRAllProject.Checked)
            {
                FR.Text = "تقرير مختصر جميع المشروعات";
                var LD = _Provm.LoadProjectrpt(DTF,DTT,Prtname,SPN);
                Objrpt = new AllSummryproject();
                foreach (var item in LD)
                {
                    var Tax = item.Amountvat;
                    var Cred = Convert.ToDouble(item.Creditor)/1.15;
                    var Dibt = Convert.ToDouble(item.Debitor) - Convert.ToDouble(item.Amountvat);
                    var Blbnce = Cred - Dibt;
                    var GCred = Math.Round(Cred,2);
                    var GDibt = Math.Round(Dibt, 2);
                    var GBlbnce = Math.Round(Blbnce, 2);
                    Dsx.Summryproject.Rows.Add(new object[] {
                        item.Partnername,
                        item.Projectname,
                        GCred.ToString(),
                        GDibt.ToString(),
                        GBlbnce.ToString(),
                        Convert.ToDateTime(item.Date).ToString("yyyy-MM-dd"),
                    });
                }
            }
            else if (RBDetProjects.Checked)
            {
                FR.Text = "تقرير تفصيلي جميع المشروعات";
                var LD = _Provm.LoadProjectrptdt(DTF, DTT, Prtname, SPN);
                Objrpt = new AllDetaileprojects();
                foreach (var item in LD)
                {
                    Dsx.Projectsrpt.Rows.Add(new object[] {
                        item.Partnername,
                        item.Projectname,
                        item.Creditor,
                        item.Debitor,
                        item.Vatamount,
                        Convert.ToDateTime(item.TDate).ToString("yyyy-MM-dd"),
                        item.Detailes,
                        item.ID,
                        item.Balance,
                        item.Remining,
                        item.TotCreditor,
                        item.TotDebitor,
                        item.TotVat,
                        item.TotBalance,
                        item.TotRemining,
                });
                }
            }
            else
            {

                    MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار التقرير");
                    return;
            }
            Objrpt.SetDataSource(Dsx);
            Objrpt.SetParameterValue("CompanyName", "");
            Objrpt.SetParameterValue("Taxnum", "");
            Objrpt.SetParameterValue("Proname", "");
            Objrpt.SetParameterValue("English_Shop_name", "");
            if (DTF == "" && DTT == "") 
            {
                if (CBRAllProject.Checked)
                {
                    Objrpt.SetParameterValue("SalesDate", "تقرير مختصر لجميع المشروعات");
                }
                if (RBDetProjects.Checked)
                {
                    Objrpt.SetParameterValue("SalesDate", "تقرير تفصيلي لجميع المشروعات");
                }
            }
            else
            {
                Objrpt.SetParameterValue("SalesDate", $"من {DTF} " + " " + $" إلي {DTT}");
            }
            FR.CRV.ReportSource = Objrpt;
            FR.Show();
        }

        private void EntryBtnsearch_Click(object sender, EventArgs e)
        {
            var Typeentry = "";
            var Entryscreen = "";
            int Entryuser;
            var datefrom = "";
            var dateto = "";

            if (Chkentrytype.Checked && 
                Chkscreenname.Checked &&
                Chkuser.Checked &&
                Chkdate.Checked)
            {
                Typeentry = Cmptype.Text;
                Entryscreen = Cmpscreen.Text;
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                datefrom = DTPFrom.Value.ToShortDateString();
                dateto = DTPTo.Value.ToShortDateString();
                var data = _OVM.Getall(Typeentry, Entryscreen, Entryuser, datefrom, dateto);
                DGVEntry.DataSource = data;
            }

            else if (Chkentrytype.Checked &&
                Chkscreenname.Checked &&
                Chkuser.Checked)
            {
                Typeentry = Cmptype.Text;
                Entryscreen = Cmpscreen.Text;
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                var data = _OVM.GetTSU(Typeentry, Entryscreen, Entryuser);
                DGVEntry.DataSource = data;
            }
            else if (Chkentrytype.Checked && Chkscreenname.Checked)
            {
                Typeentry = Cmptype.Text;
                Entryscreen = Cmpscreen.Text;
                var data = _OVM.GetTS(Typeentry, Entryscreen);
                DGVEntry.DataSource = data;
            }
            else if (Chkentrytype.Checked && Chkuser.Checked)
            {
                Typeentry = Cmptype.Text;
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                var data = _OVM.Gettu(Typeentry,Entryuser);
                DGVEntry.DataSource = data;
            }
            else if (Chkentrytype.Checked && Chkdate.Checked)
            {
                Typeentry = Cmptype.Text;
                datefrom = DTPFrom.Value.ToShortDateString();
                dateto = DTPTo.Value.ToShortDateString();
                var data = _OVM.Gettd(Typeentry,datefrom, dateto);
                DGVEntry.DataSource = data;
            }

            else if (Chkscreenname.Checked && Chkuser.Checked)
            {
                Entryscreen = Cmpscreen.Text;
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                var data = _OVM.Getsu(Entryscreen, Entryuser);
                DGVEntry.DataSource = data;
            }
            else if (Chkscreenname.Checked && Chkdate.Checked)
            {
                Entryscreen = Cmpscreen.Text;
                datefrom = DTPFrom.Value.ToShortDateString();
                dateto = DTPTo.Value.ToShortDateString();
                var data = _OVM.Getsd(Entryscreen, datefrom, dateto);
                DGVEntry.DataSource = data;
            }

            else if (Chkuser.Checked && Chkdate.Checked)
            {
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                datefrom = DTPFrom.Value.ToShortDateString();
                dateto = DTPTo.Value.ToShortDateString();
                var data = _OVM.Getud(Entryuser, datefrom, dateto);
                DGVEntry.DataSource = data;
            }

            else if (Chkentrytype.Checked)
            {
                Typeentry = Cmptype.Text;
                var data = _OVM.GetT(Typeentry);
                DGVEntry.DataSource = data;
            }
            else if (Chkscreenname.Checked)
            {
                Entryscreen = Cmpscreen.Text;
                var data = _OVM.GetS(Entryscreen);
                DGVEntry.DataSource = data;
            }
            else if (Chkuser.Checked)
            {
                Entryuser = int.Parse(Cmpuser.SelectedValue.ToString());
                var data = _OVM.Getu(Entryuser);
                DGVEntry.DataSource = data;
            }
            else if (Chkdate.Checked)
            {
                datefrom = DTPFrom.Value.ToShortDateString();
                dateto = DTPTo.Value.ToShortDateString();
                var data = _OVM.Getdate(datefrom, dateto);
                DGVEntry.DataSource = data;
            }

            else
            {
                Clearfileds();
                return;
            }
        }
        public void Transcases()
        {
            var dettrans = "";
            var Pro = 0;
            decimal diptsearchfom = 0;
            decimal diptsearchto = 0;
            decimal credsearchfrom = 0;
            decimal credsearchto = 0;
            var datesearchfrom = "";
            var datesearchto = "";
            if (Chkdettrans.Checked)
            {
                dettrans = txttranssearch.Text;
            }
            if (Chkpro.Checked)
            {
                Pro = int.Parse(Cmpprosearch.SelectedValue.ToString());
            }
            if (Chkdiptsearch.Checked)
            {
                diptsearchfom = decimal.Parse(txtcreditfrom.Text);
                diptsearchto = decimal.Parse(txtcreditto.Text);
            }
            if (Chkcreditsearch.Checked)
            {
                credsearchfrom = decimal.Parse(txtdiptfrom.Text);
                credsearchto = decimal.Parse(txtcreditto.Text);
            }
            if (Chkdatesearch.Checked)
            {
                datesearchfrom = Dtpsearchfrom.Value.ToString("yyyy-MM-dd");
                datesearchto = Dtpsearchto.Value.ToString("yyyy-MM-dd");
            }
            //Cases
            //All
            if (dettrans != "" && Pro != 0 &&
                diptsearchfom != 0 && diptsearchto != 0 &&
                credsearchfrom != 0 && credsearchto != 0 &&
                datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.GettransAll(dettrans, Pro,
                                             diptsearchfom, diptsearchto,
                                             credsearchfrom, credsearchto,
                                             datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;

            }
            //dettrans
            else if (dettrans != "" && datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.Gettransbydetailebydate(dettrans, datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.GettransbydetaileAndCred(dettrans, credsearchfrom, credsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && diptsearchfom != 0 && diptsearchto != 0)
            {
                var data = _Transvm.GettransbydetaileAnddipt(dettrans, diptsearchfom, diptsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && Pro != 0 &&
                     datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.GettransbydetaileAndProbydate(dettrans, Pro, datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && Pro != 0 && credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.GettransbydetaileAndProAndCred(dettrans, Pro, credsearchfrom, credsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && Pro != 0 &&
                     diptsearchfom != 0 && diptsearchto != 0)
            {
                var data = _Transvm.GettransbydetaileAndProAnddipt(dettrans, Pro, diptsearchfom, diptsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "" && Pro != 0)
            {
                var data = _Transvm.GettransbydetaiAndPro(dettrans, Pro);
                DGVTrasnaction.DataSource = data;
            }
            //Pro
            else if (Pro != 0 && datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.GettransbyProbydate(Pro, datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (Pro != 0 && credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.GettransbyProAndCred(Pro, credsearchfrom, credsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (Pro != 0 && diptsearchfom != 0 && diptsearchto != 0)
            {
                var data = _Transvm.GettransbyProanddipt(Pro, diptsearchfom, diptsearchto);
                DGVTrasnaction.DataSource = data;
            }
            //diptor
            else if (diptsearchfom != 0 && diptsearchto != 0 &&
                     credsearchfrom != 0 && credsearchto != 0 &&
                     datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.GettransGettransbydiptandcredanddate(diptsearchfom, diptsearchto,
                             credsearchfrom, credsearchto,
                             datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (diptsearchfom != 0 && diptsearchto != 0 &&
                     credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.Gettransbydiptandcred(diptsearchfom, diptsearchto,
                             credsearchfrom, credsearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (diptsearchfom != 0 && diptsearchto != 0)
            {
                var data = _Transvm.Gettransbydipt(diptsearchfom, diptsearchto);
                DGVTrasnaction.DataSource = data;
            }
            //creditor
            else if (datesearchfrom != "" && datesearchto != "" &&
                     credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.Gettransbycredanddate(credsearchfrom, credsearchto,
                             datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (credsearchfrom != 0 && credsearchto != 0)
            {
                var data = _Transvm.Gettransbycred(credsearchfrom, credsearchto);
                DGVTrasnaction.DataSource = data;
            }
            //Date
            else if (datesearchfrom != "" && datesearchto != "")
            {
                var data = _Transvm.Gettransbydate(datesearchfrom, datesearchto);
                DGVTrasnaction.DataSource = data;
            }
            else if (Pro != 0)
            {
                var data = _Transvm.GettransbyPro(Pro);
                DGVTrasnaction.DataSource = data;
            }
            else if (dettrans != "")
            {
                var data = _Transvm.Gettransbydetaile(dettrans);
                DGVTrasnaction.DataSource = data;
            }
            else
            {
                Loadlist();
                return;
            }
        }
        private void Btntranssearch_Click(object sender, EventArgs e)
        {
            Transcases();
        }
        private void Btnuserdel_Click(object sender, EventArgs e)
        {
            var Usrid = int.Parse(usrtxtnumber.Text);
            if (string.IsNullOrEmpty(usrtxtnumber.Text))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار المستخدم");
                return;
            }
            else
            {
                var DelUsr = _Usr.DeleteUsers(Usrid);
                if (DelUsr)
                {
                    Clearfileds();
                    MA.Alert("حذف المستخدم", Messagealert.enmType.Success, "تمت الحذف بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم حذف المستخدم");
                    return;
                }
            }
        }
        private void Btnusersave_Click(object sender, EventArgs e)
        {
            Usr.Username = txtusername.Text.Trim();
            Usr.Password = txtpassword.Text.Trim();
            if (string.IsNullOrEmpty(Usr.Username) || string.IsNullOrEmpty(Usr.Password))
            {
                MA.Alert("خطأ", Messagealert.enmType.Error, "برجاء ادخال اسم المستخدم وكلمة السر");
                return;
            }
            if (Btnusersave.Text == "تعديل")
            {
                Usr.Id = int.Parse(usrtxtnumber.Text);
                var UVM = _Usr.AddEditUsers(Usr);
                if (UVM)
                {
                    Clearfileds();
                    MA.Alert("تعديل مستخدم", Messagealert.enmType.Success, "تمت التعديل بنجاح");
                }
            }
            else
            {
                var UVM = _Usr.AddEditUsers(Usr);
                if (UVM)
                {
                    Clearfileds();
                    MA.Alert("ادخال مستخدم", Messagealert.enmType.Success, "تمت الادخال بنجاح");
                }
            }
        }
        private void Btnuserclear_Click(object sender, EventArgs e)
        {
            Clearfileds();
        }
        private void ProBtnsearch_Click(object sender, EventArgs e)
        {
            var Proname = txtpronamesearch.Text;
            var Amount = txtamountsearch.Text;
            decimal Amountval = 0;
            var Prtname = "";
            if (string.IsNullOrEmpty(txtamountsearch.Text))
            {
                Amountval = 0;
            }
            else
            {
                Amountval = decimal.Parse(txtamountsearch.Text);
            }
            if (Chkprtsearch.SelectedIndex != 0)
            {
                Prtname = Chkprtsearch.Text;
            }
            if (string.IsNullOrEmpty(txtpronamesearch.Text) && string.IsNullOrEmpty(txtamountsearch.Text) && Chkprtsearch.SelectedIndex == 0)
            {
                Clearfileds();
                return;
            }
            else
            {
                var Prolist = _Provm.GetPartnerName(Proname, Amountval, Prtname);
                DGVProject.DataSource = Prolist;
            }
        }
        private void Btntransdel_Click(object sender, EventArgs e)
        {
            var Transid = int.Parse(txttransnum.Text);
            if (string.IsNullOrEmpty(txttransnum.Text))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار المشروع");
                return;
            }
            else
            {
                var DelTransid = _Transvm.DeleteTrsanactions(Transid);
                if (DelTransid)
                {
                    OPR.Oprationname = "delete at";
                    OPR.Detailes = "تم حذف المعاملة في";
                    OPR.Tblname = "Tbl_Transaction";
                    OPR.Date = DateTime.Now;
                    OPR.Time = DateTime.Now.ToShortTimeString();
                    OPR.Tblid = Transid;
                    OPR.Usrid = Staffid;
                    var Opr = _OVM.AddEditOprations(OPR);
                    Clearfileds();
                    MA.Alert("حذف التعامل", Messagealert.enmType.Success, "تمت الحذف بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم حذف التعامل");
                    return;
                }
            }
        }
        private void Btntransaddedit_Click(object sender, EventArgs e)
        {
            Trans.Creditor = decimal.Parse(txttranscred.Text);
            Trans.Debitor = decimal.Parse(txttransdept.Text);
            Trans.TDate = transDTP.Value;
            Trans.Detailes = txttransnote.Text;
            Trans.Vatamount = decimal.Parse(transtxtvat.Text);
            Trans.Proid = int.Parse(Cmppro.SelectedValue.ToString());
            if (Cmppro.SelectedIndex == 0)
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء ادخال اسم المشروع");
                return;
            }
            if (Btntransaddedit.Text == "تعديل")
            {
                var Prtid = int.Parse(txttransnum.Text);
                Trans.ID = Prtid;
                var EditPrt = _Transvm.EditTrsanactions(Trans);
                if (EditPrt)
                {
                    var EditPrj = _Provm.EditProjects(Prj);
                    OPR.Oprationname = "modifaied at";
                    OPR.Detailes = "تم تعديل المعاملة في";
                    OPR.Tblname = "Tbl_Transaction";
                    OPR.Date = DateTime.Now;
                    OPR.Time = DateTime.Now.ToShortTimeString();
                    OPR.Tblid = Prtid;
                    OPR.Usrid = Staffid;
                    var Opr = _OVM.AddEditOprations(OPR);
                    Clearfileds();
                    MA.Alert("تعديل معاملة", Messagealert.enmType.Success, "تمت التعديل بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم تعديل المعاملة");
                    return;
                }
            }
            else
            {
                var AddTrans = _Transvm.AddTrsanactions(Trans);
                if (AddTrans)
                {
                    var GPID = _Provm.GetAddedTransaction();
                    OPR.Oprationname = "inserted at";
                    OPR.Detailes = "تم اضافة المعاملة في";
                    OPR.Tblname = "Tbl_Transaction";
                    OPR.Date = DateTime.Now;
                    OPR.Time = DateTime.Now.ToShortTimeString();
                    OPR.Tblid = GPID;
                    OPR.Usrid = Staffid;
                    var Opr = _OVM.AddEditOprations(OPR);
                    Clearfileds() ;
                    MA.Alert("اضافة معاملة", Messagealert.enmType.Success, "تمت الاضافة بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم اضافة المعاملة");
                    return;
                }
            }
        }
        private void Btntransclear_Click(object sender, EventArgs e)
        {
            Clearfileds();
        }
        private void ProBtnDelet_Click(object sender, EventArgs e)
        {
            var Proid = int.Parse(Protxtnum.Text);
            if (string.IsNullOrEmpty(Protxtnum.Text))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار المشروع");
                return;
            }
            else
            {
                var DelProid = _Provm.DeleteProjects(Proid);
                if (DelProid)
                {
                    OPR.Oprationname = "delete at";
                    OPR.Detailes = "تم حذف المشروع في";
                    OPR.Tblname = "Project";
                    OPR.Date = DateTime.Now;
                    OPR.Time = DateTime.Now.ToShortTimeString();
                    OPR.Tblid = Proid;
                    OPR.Usrid = Staffid;
                    var Opr = _OVM.AddEditOprations(OPR);
                    Clearfileds();
                    MA.Alert("حذف مشروع", Messagealert.enmType.Success, "تمت الحذف بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم حذف المشروع وذلك لأرتباطه بعملية اخرى");
                    return;
                }
            }
        }
        private void ProBtnclear_Click(object sender, EventArgs e)
        {
            Clearfileds();
        }
        private void PrtBtndelete_Click(object sender, EventArgs e)
        {
            var Prtid = int.Parse(Prttxtnumber.Text);
            if (string.IsNullOrEmpty(Prttxtnumber.Text))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار الشريك");
                return;
            }
            else
            {
                var DelPrt = _PVM.DeletePartner(Prtid);
                if (DelPrt)
                {
                    Clearfileds();
                    MA.Alert("حذف شريك", Messagealert.enmType.Success, "تمت الحذف بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم حذف الشريك");
                    return;
                }
            }
        }
        private void Clearfileds()
        {
            Loadlist();
            Loadcombo();
            //LoadProjects();
            Loadcombo1();
            Loadcombo2();
            Loadcombotrans1();
            Loadcombotrans2();
            Loadcomuser();
            LoadProjectsCLP();
            //LoadProjectrpt();
            txttransnum.Clear();
            Cmptype.SelectedIndex = 0;
            Cmpscreen.SelectedIndex = 0;
            usrtxtnumber.Clear();
            txtusername.Clear();
            txtpassword.Clear();
            txtusersearch.Clear();
            txttranscred.Text = "0.00";
            txttransdept.Text = "0.00";
            transDTP.Text = DateTime.Now.ToString();
            transtxtvat.Text = "0.00";
            txttransnote.Clear();
            Cmppro.SelectedIndex = 0;
            Chktransvat.Checked = false;
            Chkdettrans.Checked = false;
            txttranssearch.Clear();
            Chkdiptsearch.Checked = false;
            txtcreditfrom.Clear();
            txtcreditto.Clear();
            Chkdatesearch.Checked = false;
            Dtpsearchfrom.Text = DateTime.Now.ToString();
            Dtpsearchto.Text = DateTime.Now.ToString();
            Chkpro.Checked = false;
            Cmpprosearch.SelectedIndex = 0;
            Chkcreditsearch.Checked = false;
            txtdiptfrom.Clear();
            txtdiptto.Clear();
            Prttxtnumber.Clear();
            Protxtnum.Clear();
            Prttxtname.Clear();
            Protxtname.Clear();
            Protxtamount.Text = "0.00";
            Protxtvat.Text = "0.00";
            Prochk.Checked = false;
            Cmbpro.SelectedIndex = 0;
            Protxtnote.Clear();
            txtpronamesearch.Clear();
            txtamountsearch.Clear();
            Chkprtsearch.SelectedIndex = 0;
            Prttxtnote.Clear();
            PrtBtnsaveanmodi.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            ProBtnSaveAndmodi.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            Btntransaddedit.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            Btnusersave.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            PrtBtnsaveanmodi.Text = "حفظ";
            ProBtnSaveAndmodi.Text = "حفظ";
            Btntransaddedit.Text = "حفظ";
            Btnusersave.Text = "حفظ";
            PrtBtnsaveanmodi.BackColor = Color.FromArgb(0, 173, 31);
            ProBtnSaveAndmodi.BackColor = Color.FromArgb(0, 173, 31);
            Btntransaddedit.BackColor = Color.FromArgb(0, 173, 31);
            Btnusersave.BackColor = Color.FromArgb(0, 173, 31);
        }

        private void Loadcombo2()
        {
            var Prtlist = _PVM.LoadPartners();
            Prtlist.Insert(0, new Partner() { ID = 0, Partnername = "-- اسم الشريك --" });
            Chkprtsearch.DropDownStyle = ComboBoxStyle.DropDownList;
            Chkprtsearch.DisplayMember = "Partnername";
            Chkprtsearch.ValueMember = "ID";
            Chkprtsearch.DataSource = Prtlist;
            Chkprtsearch.Invalidate();
            Chkprtsearch.SelectedIndex = 0;
        }
        //CLBProject
        private void Loadcombo()
        {
            var Prtlist = _PVM.LoadPartners();
            Prtlist.Insert(0, new Partner() { ID = 0, Partnername = "-- اسم الشريك --" });
            Cmbpro.DropDownStyle = ComboBoxStyle.DropDownList;

            Cmbpro.DisplayMember = "Partnername";
            Cmbpro.ValueMember = "ID";
            Cmbpro.DataSource = Prtlist;
            Cmbpro.Invalidate();
            Cmbpro.SelectedIndex = 0;
        }
        private void Loadcombo1()
        {
            var Prtlist = _PVM.LoadPartners();
            Prtlist.Insert(0, new Partner() { ID = 0, Partnername = "-- الكل --" });
            Partnercmb.DropDownStyle = ComboBoxStyle.DropDownList;

            Partnercmb.DisplayMember = "Partnername";
            Partnercmb.ValueMember = "ID";
            Partnercmb.DataSource = Prtlist;
            Partnercmb.Invalidate();
            Partnercmb.SelectedIndex = 0;
        }
        private void Loadcombotrans1()
        {
            var Prolist = _Provm.LoadProjectcombo1();
            Prolist.Insert(0, new Projectdata() { ID = 0, Projectname = "-- اسم المشروع --" });
            Cmpprosearch.DropDownStyle = ComboBoxStyle.DropDownList;
            Cmpprosearch.DisplayMember = "Projectname";
            Cmpprosearch.ValueMember = "ID";
            Cmpprosearch.DataSource = Prolist;
            Cmpprosearch.Invalidate();
            Cmpprosearch.SelectedIndex = 0;
        }
        private void Loadcomuser()
        {
            var Userlist = _Usr.LoadUsercombo();
            Userlist.Insert(0, new User() { Id = 0, Username = "-- اسم المستخدم --" });
            Cmpuser.DropDownStyle = ComboBoxStyle.DropDownList;
            Cmpuser.DisplayMember = "Username";
            Cmpuser.ValueMember = "Id";
            Cmpuser.DataSource = Userlist;
            Cmpuser.Invalidate();
            Cmpuser.SelectedIndex = 0;
        }
        private void Loadcombotrans2()
        {
            var Prolist = _Provm.LoadProjectcombo2();
            Prolist.Insert(0, new Projectdata() { ID = 0, Projectname = "-- اسم المشروع --" });
            Cmppro.DropDownStyle = ComboBoxStyle.DropDownList;
            Cmppro.DisplayMember = "Projectname";
            Cmppro.ValueMember = "ID";
            Cmppro.DataSource = Prolist;
            Cmppro.Invalidate();
            Cmppro.SelectedIndex = 0;
        }
        //private void LoadProjects() {
        //    var Prolist = _Provm.LoadProjectcmb();
        //    Prolist.Insert(0, new Projectdata() { ID = 0, Projectname = "-- الكل --" });
        //    Projectcmb.DropDownStyle = ComboBoxStyle.DropDownList;
        //    Projectcmb.DisplayMember = "Projectname";
        //    Projectcmb.ValueMember = "ID";
        //    Projectcmb.DataSource = Prolist;
        //    Projectcmb.Invalidate();
        //    Projectcmb.SelectedIndex = 0;
        //}
        private void LoadProjectsCLP()
        {
            var Prolist = _Provm.LoadProjectcmb();
            for (int i = 0; i < Prolist.Count; i++)
            {
                CLBProject.Items.Add(Prolist[i].Projectname.ToString());
                CLBProject.SetItemChecked(i, true);
            }
        }
        private void Loadlist()
        {
            var Prtlist = _PVM.LoadPartners();
            DGVPartners.DataSource = Prtlist;

            var Prolist = _Provm.LoadProjects();
            DGVProject.DataSource = Prolist;

            var Trnslist = _Transvm.LoadTrsanactions();
            DGVTrasnaction.DataSource = Trnslist;

            var OVM = _OVM.LoadOprations();
            DGVEntry.DataSource = OVM;

            var Usr = _Usr.LoadUsers();
            DGVUsers.DataSource = Usr;
        }
        private void ProBtnSaveAndmodi_Click(object sender, EventArgs e)
        {
            Prj.Projectname = Protxtname.Text.Trim();
            Prj.Amount = decimal.Parse(Protxtamount.Text);
            Prj.Amountvat = decimal.Parse(Protxtvat.Text);
            Prj.Prtid = int.Parse(Cmbpro.SelectedValue.ToString());
            Prj.Note = Protxtnote.Text;
            if (string.IsNullOrEmpty(Prj.Projectname))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء ادخال اسم المشروع");
                return;
            }
            else if (Cmbpro.SelectedIndex == 0)
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء اختيار اسم الشريك");
                return;
            }
            else if (ProBtnSaveAndmodi.Text == "تعديل")
            {
                var Proid = int.Parse(Protxtnum.Text);
                Prj.ID = Proid;
                var EditPrj = _Provm.EditProjects(Prj);
                OPR.Oprationname = "modifaied at";
                OPR.Detailes = "تم تعديل المشروع في";
                OPR.Tblname = "Project";
                OPR.Date = DateTime.Now;
                OPR.Time = DateTime.Now.ToShortTimeString();
                OPR.Tblid = Prj.ID;
                OPR.Usrid = Staffid;
                var Opr = _OVM.AddEditOprations(OPR);
                if (EditPrj)
                {
                    Clearfileds();
                    MA.Alert("تعديل مشروع", Messagealert.enmType.Success, "تمت التعديل بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم تعديل المشروع");
                    return;
                }
            }
            else
            {
                var AddPrj = _Provm.AddProjects(Prj);
                if (AddPrj)
                {
                    var GPID = _Provm.GetAddedproject();
                    OPR.Oprationname = "inserted at";
                    OPR.Detailes = "تم اضافة المشروع في";
                    OPR.Tblname = "Project";
                    OPR.Date = DateTime.Now;
                    OPR.Time = DateTime.Now.ToShortTimeString();
                    OPR.Tblid = GPID;
                    OPR.Usrid = Staffid;
                    var Opr = _OVM.AddEditOprations(OPR);
                    Clearfileds();
                    MA.Alert("اضافة مشروع", Messagealert.enmType.Success, "تمت الاضافة بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم اضافة مشروع");
                    return;
                }
            }
        }
        private void PrtBtnsaveanmodi_Click(object sender, EventArgs e)
        {
            Prt.Partnername = Prttxtname.Text.Trim();
            Prt.Description = Prttxtnote.Text;
            if (string.IsNullOrEmpty(Prt.Partnername))
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء ادخال اسم الشريك");
                return;
            }
            if (PrtBtnsaveanmodi.Text == "تعديل")
            {          
                var Prtid = int.Parse(Prttxtnumber.Text);
                Prt.ID = Prtid;
                var EditPrt = _PVM.AddEditPartner(Prt);
                if (EditPrt)
                {
                    Clearfileds();
                    MA.Alert("تعديل شريك", Messagealert.enmType.Success, "تمت التعديل بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم تعديل الشريك");
                    return;
                }
            }         
            else
            {
                var AddPrt = _PVM.AddEditPartner(Prt);
                if (AddPrt)
                {
                    MA.Alert("اضافة شريك", Messagealert.enmType.Success, "تمت الاضافة بنجاح");
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "لم يتم اضافة الشريك");
                    return;
                }
            }
        }
        private void Protxtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e,sender);
            if (e.KeyChar == (char)13)
            {
                Prochk.Focus();
            }
        }
        private void Protxtvat_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void txtamountsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void transtxtvat_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void txttranscred_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
            if (e.KeyChar == (char)13)
            {
                txttransdept.Focus();
            }
        }
        private void txttransdept_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
            if (e.KeyChar == (char)13)
            {
                Chktransvat.Focus();
            }
        }
        private void txtcreditfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void txtcreditto_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void txtdiptfrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void txtdiptto_KeyPress(object sender, KeyPressEventArgs e)
        {
            _OC.Usenumber(e, sender);
        }
        private void DGVPartners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVPartners.Rows.Count > 0)
            {
                PrtBtnsaveanmodi.IconChar = FontAwesome.Sharp.IconChar.Pen;
                PrtBtnsaveanmodi.Text = "تعديل";
                PrtBtnsaveanmodi.BackColor = Color.FromArgb(255, 184, 128);
                Prttxtnumber.Text = DGVPartners.CurrentRow.Cells[0].Value.ToString();
                Prttxtname.Text = DGVPartners.CurrentRow.Cells[1].Value.ToString();
                Prttxtnote.Text = DGVPartners.CurrentRow.Cells[2].Value?.ToString();
            }
        }
        private void PrtBtnclear_Click(object sender, EventArgs e)
        {
            Clearfileds();
        }
        private void Prttxtsearch_TextChanged(object sender, EventArgs e)
        {
            var Prtlist = _PVM.GetPartnerName(Prttxtsearch.Text);
            DGVPartners.DataSource = Prtlist;
        }
        private void DGVProject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVProject.Rows.Count > 0)
            {
                ProBtnSaveAndmodi.IconChar = FontAwesome.Sharp.IconChar.Pen;
                ProBtnSaveAndmodi.Text = "تعديل";
                ProBtnSaveAndmodi.BackColor = Color.FromArgb(255, 184, 128);
                Protxtnum.Text = DGVProject.CurrentRow.Cells[0].Value.ToString();
                Protxtname.Text = DGVProject.CurrentRow.Cells[1].Value.ToString();
                Protxtamount.Text = DGVProject.CurrentRow.Cells[2].Value.ToString();
                Protxtvat.Text = DGVProject.CurrentRow.Cells[3].Value?.ToString();
                Cmbpro.Text = DGVProject.CurrentRow.Cells[4].Value?.ToString();
                Protxtnote.Text = DGVProject.CurrentRow.Cells[5].Value?.ToString();
            }
        }
        private void DGVTrasnaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVTrasnaction.Rows.Count > 0)
            {
                Btntransaddedit.IconChar = FontAwesome.Sharp.IconChar.Pen;
                Btntransaddedit.Text = "تعديل";
                Btntransaddedit.BackColor = Color.FromArgb(255, 184, 128);
                txttransnum.Text = DGVTrasnaction.CurrentRow.Cells[0].Value.ToString();
                txttranscred.Text = DGVTrasnaction.CurrentRow.Cells[1].Value.ToString();
                txttransdept.Text = DGVTrasnaction.CurrentRow.Cells[2].Value.ToString();
                transDTP.Text = DGVTrasnaction.CurrentRow.Cells[3].Value?.ToString();
                txttransnote.Text = DGVTrasnaction.CurrentRow.Cells[4].Value?.ToString();
                transtxtvat.Text = DGVTrasnaction.CurrentRow.Cells[5].Value?.ToString();
                Cmppro.Text = DGVTrasnaction.CurrentRow.Cells[6].Value?.ToString();
            }
        }
        private void DGVUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVUsers.Rows.Count > 0)
            {
                Btnusersave.IconChar = FontAwesome.Sharp.IconChar.Pen;
                Btnusersave.Text = "تعديل";
                Btnusersave.BackColor = Color.FromArgb(255, 184, 128);
                usrtxtnumber.Text = DGVUsers.CurrentRow.Cells[0].Value.ToString();
                txtusername.Text = DGVUsers.CurrentRow.Cells[1].Value.ToString();
                txtpassword.Text = DGVUsers.CurrentRow.Cells[2].Value.ToString();
            }
        }
        private void txtusersearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusersearch.Text))
            {
                Loadlist();
            }
            else
            {
                var Usrlist = _Usr.GetUsers(txtusersearch.Text);
                DGVUsers.DataSource = Usrlist;
            }
        }
        private void Prochk_CheckedChanged(object sender, EventArgs e)
        {
            if(Prochk.Checked)
            {
                if (Protxtamount.Text != "0.00")
                {
                    var Amount = (double.Parse(Protxtamount.Text) * 0.15);
                    var GAmount = Math.Round(Convert.ToDouble(Amount), 2).ToString();
                    Protxtvat.Text = GAmount;
                }
            }
            else
            {
                Protxtvat.Text = "0.00";
            }
        }
        private void Chktransvat_CheckedChanged(object sender, EventArgs e)
        {
            if (Chktransvat.Checked)
            {
                if (txttranscred.Text != "0.00")
                {
                    txttransdept.Text = "0.00";
                    var Amount = (double.Parse(txttranscred.Text) * 0.15);
                    var GAmount = Math.Round(Convert.ToDouble(Amount), 2).ToString();
                    transtxtvat.Text = GAmount;
                }
                if (txttransdept.Text != "0.00")
                {
                    txttranscred.Text = "0.00";
                    var Amount = (double.Parse(txttransdept.Text) * 0.15);
                    var GAmount = Math.Round(Convert.ToDouble(Amount), 2).ToString();
                    transtxtvat.Text = GAmount;
                }
            }
            else
            {
                transtxtvat.Text = "0.00";
            }
        }
        private void txttranscred_TextChanged(object sender, EventArgs e)
        {
            if (txttransdept.Text != "0.00")
            {
                txttransdept.Text = "0.00";
            }
        }
        private void txttransdept_TextChanged(object sender, EventArgs e)
        {
            if (txttranscred.Text != "0.00")
            {
                txttranscred.Text = "0.00";
            }
        }

        private void Prttxtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Prttxtnote.Focus();
            }
        }

        private void Protxtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Protxtamount.Focus();
            }
        }

        private void Prochk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cmbpro.Focus();
            }
        }

        private void Cmbpro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Protxtnote.Focus();
            }
        }

        private void Chktransvat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cmppro.Focus();
            }
        }

        private void Cmppro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txttransnote.Focus();
            }
        }

        private void Chkentrytype_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cmptype.Focus();
            }
        }

        private void Cmptype_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Chkscreenname.Focus();
            }
        }

        private void Chkscreenname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cmpscreen.Focus();
            }
        }

        private void Cmpscreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Chkuser.Focus();
            }
        }

        private void Chkuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Cmpuser.Focus();
            }
        }

        private void Cmpuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Chkdate.Focus();
            }
        }

        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                txtpassword.Focus();
            }
        }
    }
}
