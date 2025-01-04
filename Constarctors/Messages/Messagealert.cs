using Constarctors.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Constractors
{
    public partial class Messagealert : Form
    {
        public string Alertmsg { get; set; }
        public string Messagestring { get; set; }
        public Messagealert.enmType Type { get; set; }
        public Messagealert()
        {
            InitializeComponent();
        }
        public void Alert(string title, Messagealert.enmType type, string msg)
        {
            Messagealert frm = new Messagealert();
            frm.showAlert(msg, type,title);
        }
        public enum enmAction
        {
            wait,
            start,
            close
        }
        public enum enmType
        {
            Success,
            Warning,
            Error,
            Info
        }
        private Messagealert.enmAction action;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tmr.Interval = 1;
            action = enmAction.close;
        }
        private void Messagealert_Load(object sender, EventArgs e)
        {

        }
        public void showAlert(string msg, enmType type, string title)
        {
            switch(type)
            {
                case enmType.Success:
                    this.pictureBox1.Image = Resources.success;
                    this.BackColor = Color.SeaGreen;
                    break;
                case enmType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.DarkRed;
                    break;
                case enmType.Info:
                    this.pictureBox1.Image = Resources.info;
                    this.BackColor = Color.RoyalBlue;
                    break;
                case enmType.Warning:
                    this.pictureBox1.Image = Resources.warning;
                    this.BackColor = Color.DarkOrange;
                    break;
            }
            this.lblMsgtitle.Text = title;
            this.lblMegpargraph.Text = msg;
            this.Show();
            this.action = enmAction.start;
            this.tmr.Start();
        }
        private void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            this.Close();
        }
    }
}
