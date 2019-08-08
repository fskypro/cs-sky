// ------------------------------------------------------------------
// Description : JSON 数据根节点，对应一个 JSON 配置文件
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace XTreme.XTJson
{
	public class XTJsonRoot : XTJsonDict
	{
		private string m_path;
		private Encoding m_encoding;
		private List<XTJsonComment> m_doc;

		internal XTJsonRoot(string path, Encoding enc, List<XTJsonComment> doc = null)
			:base(new Dictionary<XTJsonKeyable, XTJsonData>())
		{
			this.m_path = path;
			this.m_encoding = enc;
			this.m_doc = doc;
		}

		internal XTJsonRoot(string path, XTJsonDict jdict, Encoding enc, List<XTJsonComment> doc = null)
			: base(jdict)
		{
			this.m_path = path;
			this.m_encoding = enc;
			this.m_doc = doc;
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		public string Path
		{
			get { return this.m_path; }
		}

		public Encoding Encoding
		{
			get { return this.m_encoding; }
		}

		public List<XTJsonComment> Document
		{
			get { return this.m_doc; }
			set { this.m_doc = value; }
		}

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		public void Save(uint warps = 1)
		{
			XTJson.Write(this.m_path, this, this.m_encoding, warps, this.m_doc);
		}

		public new void SaveAs(string path, uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			XTJson.Write(path, this, warps, doc, isCache);
		}

		public new void SaveAs(string path, Encoding enc, uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			XTJson.Write(path, this, enc, warps, doc, isCache);
		}
	}
}
