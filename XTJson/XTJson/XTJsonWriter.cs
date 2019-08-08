// ------------------------------------------------------------------
// Description : JSON 数据反序列化器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using DictItem = System.Collections.Generic.KeyValuePair<
	XTreme.XTJson.XTJsonKeyable, XTreme.XTJson.XTJsonData>;

namespace XTreme.XTJson
{
	 internal class XTJsonWriter
	 {
		private TextWriter m_tw;
		private readonly uint m_warps;
		private uint m_currWarp;

		public XTJsonWriter(TextWriter tw, uint warps)
		{
			this.m_tw = tw;
			this.m_warps = warps;
			this.m_currWarp = 0;
		}

		// ---------------------------------------------------------------
		// private
		// ---------------------------------------------------------------
		#region 类型写出接口
		private void WriteDoc(List<XTJsonComment> doc)
		{
			if (doc == null) return;
			foreach (XTJsonComment com in doc)
				this.m_tw.Write(com.Comment + "\n");
			this.m_tw.Write("\n");
		}

		private void WriteString(XTJsonData jdata)
		{
			this.m_tw.Write("\"");
			this.m_tw.Write(jdata.ToString());
			this.m_tw.Write("\"");
		}

		private void WriteValue(XTJsonData jdata)
		{
			this.m_tw.Write(jdata.ToString());
		}

		private void WriteJsonData(XTJsonData jdata)
		{
			switch (jdata.Type)
			{
 				case XTJsonType.String:
					this.WriteString(jdata);
					break;
				case XTJsonType.Dict:
					this.WriteDict((XTJsonDict)jdata);
					break;
				case XTJsonType.List:
					this.WriteList((XTJsonList)jdata);
					break;
				default:
					this.WriteValue(jdata);
					break;
			}
		}

		private void WriteWarpSpace(uint count)
		{
			for (int i = 0; i < count; ++i)
				this.m_tw.Write('\t');
		}

		private void WriteDict(XTJsonDict jdict)
		{
			this.m_currWarp += 1;
			bool isWarp = this.m_currWarp <= this.m_warps;
			this.m_tw.Write('{');
			int count = jdict.Count;
			int index = 0;
			foreach (DictItem item in jdict)
			{
				if (isWarp)
				{
					this.m_tw.Write('\n');
					this.WriteWarpSpace(this.m_currWarp);
				}
				this.WriteJsonData(item.Key);
				this.m_tw.Write(": ");
				this.WriteJsonData(item.Value);
				if (++index < count)
					this.m_tw.Write(isWarp ? "," : ", ");
			}
			if (isWarp)
			{
				this.m_tw.Write('\n');
				this.WriteWarpSpace(this.m_currWarp-1);
			}
			this.m_tw.Write('}');
			this.m_currWarp -= 1;
		}

		private void WriteList(XTJsonList jlist)
		{
			this.m_tw.Write("[");
			int index = 0;
			int count = jlist.Count;
			foreach (XTJsonData jdata in jlist)
			{
				index += 1;
				this.WriteJsonData(jdata);
				if (index < count)
					this.m_tw.Write(", ");
			}
			this.m_tw.Write("]");
		}

		#endregion


		// ---------------------------------------------------------------
		// public
		// ---------------------------------------------------------------
		public void Write(XTJsonDict jdict, List<XTJsonComment> doc)
		{
			this.WriteDoc(doc);
			this.WriteDict(jdict);
		}
	 }
}
