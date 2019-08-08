// ------------------------------------------------------------------
// Description : 十六进制长整形 JSON 数据
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
	public class XTJsonHexLong : XTJsonNumeric
	{
		public override XTJsonType Type { get { return XTJsonType.HLong; } }

		private long m_value;
		private bool m_upperCase;

		public XTJsonHexLong(long value, bool upperCase = false)
		{
			this.m_value = value;
			this.m_upperCase = upperCase;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region 与 long 互换
		// XTJsonHexLong 显式转换为 long
		public static explicit operator long(XTJsonHexLong data)
		{
			return data.m_value;
		}

		// long 隐式转换为 XTJsonHexLong
		public static implicit operator XTJsonHexLong(long value)
		{
			return new XTJsonHexLong(value);
		}

		// -------------------------------------------
		// XTJsonHexLong 与 XTJsonHexLong 比较
		public static bool operator ==(XTJsonHexLong v1, XTJsonHexLong v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonHexLong v1, XTJsonHexLong v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTJsonHexLong v1, XTJsonHexLong v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTJsonHexLong v1, XTJsonHexLong v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonHexLong 与 long 比较
		public static bool operator ==(XTJsonHexLong v1, long v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonHexLong v1, long v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTJsonHexLong v1, long v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTJsonHexLong v1, long v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// long 与 XTJsonHexLong 比较
		public static bool operator ==(long v1, XTJsonHexLong v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(long v1, XTJsonHexLong v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(long v1, XTJsonHexLong v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(long v1, XTJsonHexLong v2)
		{
			return v1 > v2.m_value;
		}

		#endregion

		#region 模拟 long
		public override string ToString()
		{
			if (this.m_upperCase)
				return string.Format("0X{0:X}", this.m_value);
			return string.Format("0x{0:x}", this.m_value);
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonHexLong)
				return this.m_value.Equals(((XTJsonHexLong)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		#endregion
	}
}
