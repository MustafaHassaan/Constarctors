using Constractors;
using Dataaccess.Models;
using Logic.ViewModel;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constarctors
{
    public partial class FRMLogin : MaterialForm
    {
        private Userviewmodel _Usr;
        private User Usr;
        Messagealert MA;
        public FRMLogin()
        {
            InitializeComponent();
            Usr = new User();
            _Usr = new Userviewmodel();
            MA = new Messagealert();
            Btnlog.Click += Btnlog_Click;
        }

        private void Btnlog_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtpassword.Text)) 
            {
                MA.Alert("خطأ", Messagealert.enmType.Warning, "برجاء ادخال اسم المستخدم وكلمة السر");
                return;
            }
            else
            {
                var UN = txtusername.Text.Trim();
                var PW = txtpassword.Text.Trim();
                var Userlog = _Usr.GetUserlog(UN, PW);
                if (Userlog)
                {
                    this.Hide();
                    Main main = new Main();
                    main.Staffid = _Usr.ID;
                    main.Staffusername = _Usr.VUsername;
                    main.Show();
                }
                else
                {
                    MA.Alert("خطأ", Messagealert.enmType.Error, "خطأ في اسم المستخدم وكلمة السر");
                    return;
                }
            }
        }

        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Btnlog.Focus();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtusername.Focus();
        }
    }
}
