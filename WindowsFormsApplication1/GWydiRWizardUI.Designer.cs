namespace WindowsFormsApplication1
{
    partial class GWydiRWizardUI
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
            this.WizardTabPanel = new System.Windows.Forms.TabControl();
            this.AuthorisationTab = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.CertificatePictureBx = new System.Windows.Forms.PictureBox();
            this.AddNewCertBtn = new System.Windows.Forms.Button();
            this.CertComboBx = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SubscriptionPictureBx = new System.Windows.Forms.PictureBox();
            this.AddNewSIDBtn = new System.Windows.Forms.Button();
            this.SIDComboBx = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NavigateNextBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.WizardTabPanel.SuspendLayout();
            this.AuthorisationTab.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CertificatePictureBx)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionPictureBx)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardTabPanel
            // 
            this.WizardTabPanel.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.WizardTabPanel.Controls.Add(this.AuthorisationTab);
            this.WizardTabPanel.Controls.Add(this.tabPage2);
            this.WizardTabPanel.Location = new System.Drawing.Point(-2, 0);
            this.WizardTabPanel.Multiline = true;
            this.WizardTabPanel.Name = "WizardTabPanel";
            this.WizardTabPanel.SelectedIndex = 0;
            this.WizardTabPanel.Size = new System.Drawing.Size(641, 481);
            this.WizardTabPanel.TabIndex = 0;
            // 
            // AuthorisationTab
            // 
            this.AuthorisationTab.Controls.Add(this.panel3);
            this.AuthorisationTab.Controls.Add(this.panel2);
            this.AuthorisationTab.Controls.Add(this.panel1);
            this.AuthorisationTab.Location = new System.Drawing.Point(23, 4);
            this.AuthorisationTab.Name = "AuthorisationTab";
            this.AuthorisationTab.Padding = new System.Windows.Forms.Padding(3);
            this.AuthorisationTab.Size = new System.Drawing.Size(614, 473);
            this.AuthorisationTab.TabIndex = 0;
            this.AuthorisationTab.Text = "Authorisation";
            this.AuthorisationTab.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.CertificatePictureBx);
            this.panel3.Controls.Add(this.AddNewCertBtn);
            this.panel3.Controls.Add(this.CertComboBx);
            this.panel3.Location = new System.Drawing.Point(10, 185);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 180);
            this.panel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Certificate";
            // 
            // CertificatePictureBx
            // 
            this.CertificatePictureBx.Location = new System.Drawing.Point(3, 3);
            this.CertificatePictureBx.Name = "CertificatePictureBx";
            this.CertificatePictureBx.Size = new System.Drawing.Size(588, 112);
            this.CertificatePictureBx.TabIndex = 5;
            this.CertificatePictureBx.TabStop = false;
            // 
            // AddNewCertBtn
            // 
            this.AddNewCertBtn.Location = new System.Drawing.Point(536, 147);
            this.AddNewCertBtn.Name = "AddNewCertBtn";
            this.AddNewCertBtn.Size = new System.Drawing.Size(48, 21);
            this.AddNewCertBtn.TabIndex = 3;
            this.AddNewCertBtn.Text = "New";
            this.AddNewCertBtn.UseVisualStyleBackColor = true;
            // 
            // CertComboBx
            // 
            this.CertComboBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CertComboBx.FormattingEnabled = true;
            this.CertComboBx.Location = new System.Drawing.Point(343, 147);
            this.CertComboBx.Name = "CertComboBx";
            this.CertComboBx.Size = new System.Drawing.Size(187, 21);
            this.CertComboBx.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.SubscriptionPictureBx);
            this.panel2.Controls.Add(this.AddNewSIDBtn);
            this.panel2.Controls.Add(this.SIDComboBx);
            this.panel2.Location = new System.Drawing.Point(10, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 171);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Subscription ID";
            // 
            // SubscriptionPictureBx
            // 
            this.SubscriptionPictureBx.Location = new System.Drawing.Point(3, 3);
            this.SubscriptionPictureBx.Name = "SubscriptionPictureBx";
            this.SubscriptionPictureBx.Size = new System.Drawing.Size(588, 112);
            this.SubscriptionPictureBx.TabIndex = 4;
            this.SubscriptionPictureBx.TabStop = false;
            // 
            // AddNewSIDBtn
            // 
            this.AddNewSIDBtn.Location = new System.Drawing.Point(536, 137);
            this.AddNewSIDBtn.Name = "AddNewSIDBtn";
            this.AddNewSIDBtn.Size = new System.Drawing.Size(48, 21);
            this.AddNewSIDBtn.TabIndex = 2;
            this.AddNewSIDBtn.Text = "New";
            this.AddNewSIDBtn.UseVisualStyleBackColor = true;
            this.AddNewSIDBtn.Click += new System.EventHandler(this.AddNewSIDBtn_Click);
            // 
            // SIDComboBx
            // 
            this.SIDComboBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SIDComboBx.FormattingEnabled = true;
            this.SIDComboBx.Location = new System.Drawing.Point(248, 137);
            this.SIDComboBx.Name = "SIDComboBx";
            this.SIDComboBx.Size = new System.Drawing.Size(282, 21);
            this.SIDComboBx.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NavigateNextBtn);
            this.panel1.Location = new System.Drawing.Point(9, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 96);
            this.panel1.TabIndex = 4;
            // 
            // NavigateNextBtn
            // 
            this.NavigateNextBtn.Location = new System.Drawing.Point(500, 40);
            this.NavigateNextBtn.Name = "NavigateNextBtn";
            this.NavigateNextBtn.Size = new System.Drawing.Size(75, 23);
            this.NavigateNextBtn.TabIndex = 0;
            this.NavigateNextBtn.Text = "Next";
            this.NavigateNextBtn.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(23, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(614, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GWydiRWizardUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 477);
            this.Controls.Add(this.WizardTabPanel);
            this.Name = "GWydiRWizardUI";
            this.Text = "GWydiR";
            this.WizardTabPanel.ResumeLayout(false);
            this.AuthorisationTab.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CertificatePictureBx)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionPictureBx)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl WizardTabPanel;
        private System.Windows.Forms.TabPage AuthorisationTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AddNewCertBtn;
        private System.Windows.Forms.Button AddNewSIDBtn;
        private System.Windows.Forms.ComboBox CertComboBx;
        private System.Windows.Forms.ComboBox SIDComboBx;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button NavigateNextBtn;
        private System.Windows.Forms.PictureBox CertificatePictureBx;
        private System.Windows.Forms.PictureBox SubscriptionPictureBx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

