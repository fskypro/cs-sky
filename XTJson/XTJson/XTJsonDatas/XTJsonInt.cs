// ------------------------------------------------------------------
// Description : 整型 JSON 数据
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
	public class XTJsonInt : XTJsonNumeric
	{
		public override XTJsonType Type { get { return XTJsonType.Int; } }

		private int m_value;

		public XTJsonInt(int value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region 与 int 互换
		// XTJsonInt 显式转换为 int
		public static explicit operator int(XTJsonInt data)
		{
			return data.m_value;
		}

		// int 隐式转换为 XTJsonInt
		public static implicit operator XTJsonInt(int value)
		{
			return new XTJsonInt(value);
		}

		// -------------------------------------------
		// XTJsonInt 与 XTJsonInt 比较
		public static bool operator ==(XTJsonInt v1, XTJsonInt v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonInt v1, XTJsonInt v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTJsonInt v1, XTJsonInt v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTJsonInt v1, XTJsonInt v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonInt 与 int 比较
		public static bool operator ==(XTJsonInt v1, int v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonInt v1, int v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTJsonInt v1, int v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTJsonInt v1, int v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// int 与 XTJsonInt 比较
		public static bool operator ==(int v1, XTJsonInt v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(int v1, XTJsonInt v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(int v1, XTJsonInt v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(int v1, XTJsonInt v2)
		{
			return v1 > v2.m_value;
		}

		#endregion

		#region 模拟 int
		public override string ToString()
		{
			return this.m_value.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonInt)
				return this.m_value.Equals(((XTJsonInt)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		#endregion
	}
}
