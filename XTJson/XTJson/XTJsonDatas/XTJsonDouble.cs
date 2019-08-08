// ------------------------------------------------------------------
// Description : 浮点型 JSON 数据
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
	public class XTJsonDouble : XTJsonNumeric
	{
		public override XTJsonType Type { get { return XTJsonType.Double; } }

		private double m_value;

		public XTJsonDouble(double value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		#region 与 double 互换
		// XTJsonDouble 显式转换为 double
		public static explicit operator double(XTJsonDouble data)
		{
			return data.m_value;
		}

		// double 隐式转换为 XTJsonDouble
		public static implicit operator XTJsonDouble(double value)
		{
			return new XTJsonDouble(value);
		}

		// -------------------------------------------
		// 两个 XTJsonDouble 比较
		public static bool operator ==(XTJsonDouble v1, XTJsonDouble v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTJsonDouble v1, XTJsonDouble v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTJsonDouble v1, XTJsonDouble v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTJsonDouble v1, XTJsonDouble v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonDouble 与 double 比较
		public static bool operator ==(XTJsonDouble v1, double v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTJsonDouble v1, double v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTJsonDouble v1, double v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTJsonDouble v1, double v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// double 与 XTJsonDouble 比较
		public static bool operator ==(double v1, XTJsonDouble v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(double v1, XTJsonDouble v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(double v1, XTJsonDouble v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(double v1, XTJsonDouble v2)
		{
			return v1 > v2.m_value;
		}

		#endregion

		#region 模拟 double
		public override string ToString()
		{
			return this.m_value.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj is XTJsonDouble)
				return this.m_value.Equals(((XTJsonDouble)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}
		#endregion
	}
}
