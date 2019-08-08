// ------------------------------------------------------------------
// Description : 字符串型 JSON 数据
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTreme.XTJson
{
	public class XTJsonString : XTJsonKeyable
	{
		public override XTJsonType Type { get { return XTJsonType.String; } }

		private string m_value;

		public XTJsonString(string value)
		{
			this.m_value = value;
		}

		public override object ToObject() { return this.m_value; }


		#region 与 string 互换

		// XTJsonString 显式转换为 string
		public static explicit operator string(XTJsonString data)
		{
			return data.m_value;
		}

		// string 隐式转换为 XTJsonString
		public static implicit operator XTJsonString(string value)
		{
			return new XTJsonString(value);
		}

		// XTJsonString 与 XTJsonString 比较
		public static bool operator ==(XTJsonString v1, XTJsonString v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonString v1, XTJsonString v2)
		{
			return v1.m_value != v2.m_value;
		}

		// XTJsonString 与 string 比较
		public static bool operator ==(XTJsonString v1, string v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonString v1, string v2)
		{
			return v1.m_value != v2;
		}

		// string 与 XTJsonString 比较
		public static bool operator ==(string v1, XTJsonString v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(string v1, XTJsonString v2)
		{
			return v1 != v2.m_value;
		}

		#endregion

		#region 模拟 string
		public override string ToString()
		{
			return this.m_value;
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonString)
				return this.m_value.Equals(((XTJsonString)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		#endregion
	}
}
