namespace Constractors
{
    partial class Messagealert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblMsgtitle = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMegpargraph = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsgtitle
            // 
            this.lblMsgtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsgtitle.AutoSize = true;
            this.lblMsgtitle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgtitle.ForeColor = System.Drawing.Color.White;
            this.lblMsgtitle.Location = new System.Drawing.Point(63, 13);
            this.lblMsgtitle.Name = "lblMsgtitle";
            this.lblMsgtitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMsgtitle.Size = new System.Drawing.Size(111, 19);
            this.lblMsgtitle.TabIndex = 0;
            this.lblMsgtitle.Text = "Message Text";
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 2000;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Constarctors.Properties.Resources.info1;
            this.pictureBox1.Location = new System.Drawing.Point(17, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblMegpargraph
            // 
            this.lblMegpargraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMegpargraph.AutoSize = true;
            this.lblMegpargraph.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMegpargraph.ForeColor = System.Drawing.Color.White;
            this.lblMegpargraph.Location = new System.Drawing.Point(62, 39);
            this.lblMegpargraph.Name = "lblMegpargraph";
            this.lblMegpargraph.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMegpargraph.Size = new System.Drawing.Size(116, 21);
            this.lblMegpargraph.TabIndex = 3;
            this.lblMegpargraph.Text = "Message Text";
            // 
            // Messagealert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(347, 74);
            this.Controls.Add(this.lblMegpargraph);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblMsgtitle);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Messagealert";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Alert";
            this.Load += new System.EventHandler(this.Messagealert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsgtitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Label lblMegpargraph;
    }
}