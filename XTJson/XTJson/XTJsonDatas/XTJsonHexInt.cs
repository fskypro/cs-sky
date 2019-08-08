// ------------------------------------------------------------------
// Description : 十六进制整型 JSON 数据
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
	public class XTJsonHexInt : XTJsonNumeric
	{
		public override XTJsonType Type { get { return XTJsonType.HInt; } }

		private int m_value;
		private bool m_upperCasel;

		public XTJsonHexInt(int value, bool upperCase=false)
		{
			this.m_value = value;
			this.m_upperCasel = upperCase;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region 与 int 互换
		// XTJsonHexInt 显式转换为 int
		public static explicit operator int(XTJsonHexInt data)
		{
			return data.m_value;
		}

		// int 隐式转换为 XTJsonHexInt
		public static implicit operator XTJsonHexInt(int value)
		{
			return new XTJsonHexInt(value);
		}

		// -------------------------------------------
		// XTJsonHexInt 与 XTJsonHexInt 比较
		public static bool operator ==(XTJsonHexInt v1, XTJsonHexInt v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonHexInt v1, XTJsonHexInt v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTJsonHexInt v1, XTJsonHexInt v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTJsonHexInt v1, XTJsonHexInt v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonHexInt 与 int 比较
		public static bool operator ==(XTJsonHexInt v1, int v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonHexInt v1, int v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTJsonHexInt v1, int v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTJsonHexInt v1, int v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// int 与 XTJsonHexInt 比较
		public static bool operator ==(int v1, XTJsonHexInt v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(int v1, XTJsonHexInt v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(int v1, XTJsonHexInt v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(int v1, XTJsonHexInt v2)
		{
			return v1 > v2.m_value;
		}

		#endregion

		#region 模拟 int
		public override string ToString()
		{
			if (this.m_upperCasel)
				return string.Format("0X{0:X}", this.m_value);
			return string.Format("0x{0:x}", this.m_value);
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonHexInt)
				return this.m_value.Equals(((XTJsonHexInt)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		#endregion
	}
}
