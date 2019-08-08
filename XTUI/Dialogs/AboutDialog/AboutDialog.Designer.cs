namespace XTreme.XTUI
{
	partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.m_lbTitle = new System.Windows.Forms.Label();
			this.m_btnClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.m_lbEmail = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.m_lbProduct = new System.Windows.Forms.Label();
			this.m_lbVersion = new System.Windows.Forms.Label();
			this.m_lbWebSite = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.m_lbCorp = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// m_lbTitle
			// 
			this.m_lbTitle.AutoSize = true;
			this.m_lbTitle.BackColor = System.Drawing.Color.Transparent;
			this.m_lbTitle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_lbTitle.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.m_lbTitle.Location = new System.Drawing.Point(32, 6);
			this.m_lbTitle.Name = "m_lbTitle";
			this.m_lbTitle.Size = new System.Drawing.Size(47, 14);
			this.m_lbTitle.TabIndex = 0;
			this.m_lbTitle.Text = "About";
			this.m_lbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.m_lbTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.m_lbTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.m_lbTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_btnClose
			// 
			this.m_btnClose.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.m_btnClose.Location = new System.Drawing.Point(276, 148);
			this.m_btnClose.Name = "m_btnClose";
			this.m_btnClose.Size = new System.Drawing.Size(51, 31);
			this.m_btnClose.TabIndex = 1;
			this.m_btnClose.Text = "Back";
			this.m_btnClose.UseVisualStyleBackColor = true;
			this.m_btnClose.Click += new System.EventHandler(this.OnCloseClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.Gainsboro;
			this.label2.Location = new System.Drawing.Point(38, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "Corp. : ";
			this.label2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.ForeColor = System.Drawing.Color.Gainsboro;
			this.label3.Location = new System.Drawing.Point(38, 127);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "Author: Nostyle";
			this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.ForeColor = System.Drawing.Color.Gainsboro;
			this.label4.Location = new System.Drawing.Point(38, 147);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "Email :";
			this.label4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_lbEmail
			// 
			this.m_lbEmail.AutoSize = true;
			this.m_lbEmail.BackColor = System.Drawing.Color.Transparent;
			this.m_lbEmail.Location = new System.Drawing.Point(95, 147);
			this.m_lbEmail.Name = "m_lbEmail";
			this.m_lbEmail.Size = new System.Drawing.Size(89, 12);
			this.m_lbEmail.TabIndex = 5;
			this.m_lbEmail.TabStop = true;
			this.m_lbEmail.Text = "4675188@qq.com";
			this.m_lbEmail.Click += new System.EventHandler(this.OnEmailClick);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label5.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.label5.Location = new System.Drawing.Point(38, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 15);
			this.label5.TabIndex = 6;
			this.label5.Text = "Version :";
			this.label5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label6.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.label6.Location = new System.Drawing.Point(38, 41);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 15);
			this.label6.TabIndex = 7;
			this.label6.Text = "Product :";
			this.label6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_lbProduct
			// 
			this.m_lbProduct.AutoSize = true;
			this.m_lbProduct.BackColor = System.Drawing.Color.Transparent;
			this.m_lbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_lbProduct.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.m_lbProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.m_lbProduct.Location = new System.Drawing.Point(124, 41);
			this.m_lbProduct.Name = "m_lbProduct";
			this.m_lbProduct.Size = new System.Drawing.Size(70, 15);
			this.m_lbProduct.TabIndex = 8;
			this.m_lbProduct.Text = "Product";
			this.m_lbProduct.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.m_lbProduct.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.m_lbProduct.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.m_lbProduct.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_lbVersion
			// 
			this.m_lbVersion.AutoSize = true;
			this.m_lbVersion.BackColor = System.Drawing.Color.Transparent;
			this.m_lbVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_lbVersion.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.m_lbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.m_lbVersion.Location = new System.Drawing.Point(124, 62);
			this.m_lbVersion.Name = "m_lbVersion";
			this.m_lbVersion.Size = new System.Drawing.Size(52, 15);
			this.m_lbVersion.TabIndex = 9;
			this.m_lbVersion.Text = "1.0.0";
			this.m_lbVersion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.m_lbVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.m_lbVersion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.m_lbVersion.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_lbWebSite
			// 
			this.m_lbWebSite.AutoSize = true;
			this.m_lbWebSite.BackColor = System.Drawing.Color.Transparent;
			this.m_lbWebSite.Location = new System.Drawing.Point(95, 107);
			this.m_lbWebSite.Name = "m_lbWebSite";
			this.m_lbWebSite.Size = new System.Drawing.Size(191, 12);
			this.m_lbWebSite.TabIndex = 11;
			this.m_lbWebSite.TabStop = true;
			this.m_lbWebSite.Text = "https://github.com/xtreme-space";
			this.m_lbWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnWebSiteClick);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.ForeColor = System.Drawing.Color.Gainsboro;
			this.label7.Location = new System.Drawing.Point(38, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 12);
			this.label7.TabIndex = 10;
			this.label7.Text = "WSite :";
			this.label7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseClick);
			this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseDown);
			this.label7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseMove);
			this.label7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnLabelMouseUp);
			// 
			// m_lbCorp
			// 
			this.m_lbCorp.AutoSize = true;
			this.m_lbCorp.BackColor = System.Drawing.Color.Transparent;
			this.m_lbCorp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_lbCorp.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.m_lbCorp.ForeColor = System.Drawing.Color.Gainsboro;
			this.m_lbCorp.Location = new System.Drawing.Point(95, 87);
			this.m_lbCorp.Name = "m_lbCorp";
			this.m_lbCorp.Size = new System.Drawing.Size(152, 12);
			this.m_lbCorp.TabIndex = 12;
			this.m_lbCorp.Text = "Xtreme Technology Co.";
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::XTUI.Properties.Resources.bg2;
			this.ClientSize = new System.Drawing.Size(340, 192);
			this.Controls.Add(this.m_lbCorp);
			this.Controls.Add(this.m_lbWebSite);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.m_lbVersion);
			this.Controls.Add(this.m_lbProduct);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.m_lbEmail);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.m_btnClose);
			this.Controls.Add(this.m_lbTitle);
			this.ForeColor = System.Drawing.Color.Green;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.Opacity = 0.95D;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label m_lbTitle;
		private System.Windows.Forms.Button m_btnClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel m_lbEmail;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label m_lbProduct;
		private System.Windows.Forms.Label m_lbVersion;
		private System.Windows.Forms.LinkLabel m_lbWebSite;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label m_lbCorp;
	}
}