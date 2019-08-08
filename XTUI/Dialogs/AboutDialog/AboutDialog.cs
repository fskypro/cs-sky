// ------------------------------------------------------------------
// Description : 通用关于窗口
// Author      : hyw
// Date        : 2014.11.26
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XTreme.XTUI
{
	public partial class AboutDialog : Form
	{
		private const string WEB_SITE = "https://github.com/xtreme-space";
		private const string EMAIL = "4675188@QQ.com";

		private Point? m_mpos;

		public AboutDialog()
		{
			InitializeComponent();
			this.MouseDown += this.OnLabelMouseDown;
			this.MouseUp += this.OnLabelMouseUp;
			this.MouseMove += this.OnLabelMouseMove;
			this.MouseClick += this.OnLabelMouseClick;

			this.m_lbWebSite.Text = WEB_SITE;
			this.m_lbEmail.Text = EMAIL;

			this.BackColor = SystemColors.Control;
			this.TransparencyKey = this.BackColor;
			this.m_mpos = null;
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		public string CorpName
		{
			get { return this.m_lbCorp.Text; }
			set { this.m_lbCorp.Text = value; }
		}

		// ----------------------------------------------------------
		// Control events
		// ----------------------------------------------------------
		private void OnLabelMouseDown(object sender, MouseEventArgs e)
		{
			Point mpos = Control.MousePosition;
			this.m_mpos = this.PointToClient(mpos);
		}

		private void OnLabelMouseUp(object sender, MouseEventArgs e)
		{
			this.m_mpos = null;
		}

		private void OnLabelMouseMove(object sender, MouseEventArgs e)
		{
			if (this.m_mpos != null)
			{
				Point ppos = Control.MousePosition;
				Point rpos = (Point)this.m_mpos;
				this.Location = new Point(ppos.X - rpos.X, ppos.Y - rpos.Y);
			}
		}

		private void OnLabelMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.Close();
			}
		}

		// -------------------------------------------
		private void OnCloseClick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void OnWebSiteClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(WEB_SITE);
		}

		private void OnEmailClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:" + EMAIL);
		}


		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		public new void Show(IWin32Window owner = null) {}
		public new void ShowDialog(IWin32Window owner = null) {}

		public void Show(IWin32Window owner, string product, string version="1.0.0")
		{
			this.m_lbProduct.Text = product;
			this.m_lbVersion.Text = version;
			base.Show(owner);
		}

		public void ShowDialog(IWin32Window owner, string product, string version = "1.0.0")
		{
			this.m_lbProduct.Text = product;
			this.m_lbVersion.Text = version;
			base.ShowDialog(owner);
		}

	}
}
