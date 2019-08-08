// ------------------------------------------------------------------
// Description : 长整形 JSON 数据
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
	public class XTJsonLong : XTJsonNumeric
	{
		public override XTJsonType Type { get { return XTJsonType.Long; } }

		private long m_value;

		public XTJsonLong(long value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region 与 long 互换

		// XTJsonLong 显式转换为 long
		public static explicit operator long(XTJsonLong data)
		{
			return data.m_value;
		}

		// long 隐式转换为 XTJsonLong
		public static implicit operator XTJsonLong(long value)
		{
			return new XTJsonLong(value);
		}

		// -------------------------------------------
		// XTJsonLong 与 XTJsonLong 比较
		public static bool operator ==(XTJsonLong v1, XTJsonLong v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonLong v1, XTJsonLong v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTJsonLong v1, XTJsonLong v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTJsonLong v1, XTJsonLong v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonLong 与 long 比较
		public static bool operator ==(XTJsonLong v1, long v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonLong v1, long v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTJsonLong v1, long v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTJsonLong v1, long v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// long 与 XTJsonLong 比较
		public static bool operator ==(long v1, XTJsonLong v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(long v1, XTJsonLong v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(long v1, XTJsonLong v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(long v1, XTJsonLong v2)
		{
			return v1 > v2.m_value;
		}

		#endregion

		#region 模拟 long
		public override string ToString()
		{
			return this.m_value.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonLong)
				return this.m_value.Equals(((XTJsonLong)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}
		#endregion
	}
}
