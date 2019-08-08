// ------------------------------------------------------------------
// Description : 布尔型 JSON 数据
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
	public class XTJsonBool : XTJsonKeyable
	{
		public override XTJsonType Type { get { return XTJsonType.Bool; } }

		private bool m_value;

		public XTJsonBool(bool value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region bool 互换
		// XTJsonBool 显式转换为 bool
		public static explicit operator bool(XTJsonBool data)
		{
			return data.m_value;
		}

		// bool 隐式转换为 XTJsonBoo
		public static implicit operator XTJsonBool(bool value)
		{
			return new XTJsonBool(value);
		}

		// 两个 XTJsonBoo 比较
		public static bool operator==(XTJsonBool v1, XTJsonBool v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator!=(XTJsonBool v1, XTJsonBool v2)
		{
			return v1.m_value != v2.m_value;
		}

		// XTJsonBool 与 boo 比较
		public static bool operator ==(XTJsonBool v1, bool v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonBool v1, bool v2)
		{
			return v1.m_value != v2;
		}

		// bool 与 XTJsonBool 比较
		public static bool operator ==(bool v1, XTJsonBool v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(bool v1, XTJsonBool v2)
		{
			return v1 != v2.m_value;
		}

		#endregion

		#region 模拟 bool
		public override string ToString()
		{
			return this.m_value.ToString().ToLower();
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonBool)
				return this.m_value.Equals(((XTJsonBool)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}
		#endregion
	}
}
