// ------------------------------------------------------------------
// Description : 数值节点
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace XTreme.XTFormula
{
	public abstract class XTNumericToken : XTFormulaToken
	{
		public abstract object ToObject();	// 返回原值
		public abstract long Round();		// 返回四舍五入值
		public abstract long Cut();			// 返回整数部分

		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			return this;
		}

		#region C# 内置类型转换为 XTNumericToken
		public static implicit operator XTNumericToken(int value)
		{
			return new XTLongToken(value);
		}

		public static implicit operator XTNumericToken(double value)
		{
			return new XTDoubleToken(value);
		}

		public static implicit operator XTNumericToken(long value)
		{
			return new XTLongToken(value);
		}

		#endregion

		#region XTNumericToken 类型转换为 C# 内置类型
		public static explicit operator int(XTNumericToken data)
		{
			return Convert.ToInt32(data.Cut());
		}

		public static explicit operator long(XTNumericToken data)
		{
			return data.Cut();
		}

		public static explicit operator float(XTNumericToken data)
		{
			return Convert.ToSingle(data.ToObject());
		}

		public static explicit operator double(XTNumericToken data)
		{
			return Convert.ToDouble(data.ToObject());
		}

		#endregion

		#region 单目运算符
		public static XTNumericToken operator -(XTNumericToken token)
		{
			if (token is XTLongToken)
				return new XTLongToken(-(long)token);
			return new XTDoubleToken(-(double)token);
		}

		#endregion

		#region 比较运算符
		public static bool operator ==(XTNumericToken lToken, XTNumericToken rToken)
		{
			if ((object)lToken == null)
				return (object)rToken == null;
			else if ((object)rToken == null)
				return (object)lToken == null;

			if (lToken is XTLongToken)
			{
				if (rToken is XTLongToken) 
					return (long)lToken == (long)rToken;
				return (long)lToken == (double)rToken;
			}
			else if (rToken is XTLongToken)
			{
				return (double)lToken == (long)rToken;
			}
			return (double)lToken == (double)rToken;
		}

		public static bool operator !=(XTNumericToken lToken, XTNumericToken rToken)
		{
			return !(lToken == rToken);
		}

		public static bool operator <(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTLongToken)
			{
				if (rToken is XTLongToken)
					return (long)lToken < (long)rToken;
				return (long)lToken < (double)rToken;
			}
			else if (rToken is XTLongToken)
			{
				return (double)lToken < (long)rToken;
			}
			return (double)lToken < (double)rToken;
		}

		public static bool operator <=(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTLongToken)
			{
				if (rToken is XTLongToken)
					return (long)lToken <= (long)rToken;
				return (long)lToken <= (double)rToken;
			}
			else if (rToken is XTLongToken)
			{
				return (double)lToken <= (long)rToken;
			}
			return (double)lToken <= (double)rToken;
		}

		public static bool operator >(XTNumericToken lToken, XTNumericToken rToken)
		{
			return !(lToken <= rToken);
		}

		public static bool operator >=(XTNumericToken lToken, XTNumericToken rToken)
		{
			return !(lToken < rToken);
		}

		#endregion

		#region 计算运算符
		public static XTNumericToken operator +(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTDoubleToken || rToken is XTDoubleToken)
				return new XTDoubleToken((double)lToken + (double)rToken);
			return new XTLongToken((long)lToken + (long)rToken);
		}

		public static XTNumericToken operator -(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTDoubleToken || rToken is XTDoubleToken)
				return new XTDoubleToken((double)lToken - (double)rToken);
			return new XTLongToken((long)lToken - (long)rToken);
		}

		public static XTNumericToken operator *(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTDoubleToken || rToken is XTDoubleToken)
				return new XTDoubleToken((double)lToken * (double)rToken);
			return new XTLongToken((long)lToken * (long)rToken);
		}

		public static XTNumericToken operator /(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTDoubleToken || rToken is XTDoubleToken)
				return new XTDoubleToken((double)lToken / (double)rToken);
			return new XTLongToken((long)lToken / (long)rToken);
		}

		public static XTNumericToken operator %(XTNumericToken lToken, XTNumericToken rToken)
		{
			if (lToken is XTDoubleToken || rToken is XTDoubleToken)
				return new XTDoubleToken((double)lToken % (double)rToken);
			return new XTLongToken((long)lToken % (long)rToken);
		}

		public static XTNumericToken operator <<(XTNumericToken lToken, int count)
		{
			return new XTLongToken((long)lToken << count);
		}

		public static XTNumericToken operator >>(XTNumericToken lToken, int count)
		{
			return new XTLongToken((long)lToken >> count);
		}

		public static XTNumericToken operator &(XTNumericToken lToken, XTNumericToken rToken)
		{
			return new XTLongToken((long)lToken & (long)rToken);
		}

		public static XTNumericToken operator |(XTNumericToken lToken, XTNumericToken rToken)
		{
			return new XTLongToken((long)lToken | (long)rToken);
		}

		public static XTNumericToken operator ^(XTNumericToken lToken, XTNumericToken rToken)
		{
			return new XTLongToken((long)lToken ^ (long)rToken);
		}

		#endregion

		#region 模拟值类型
		public override string ToString()
		{
			return "instance of XTNumericToken";
		}

		public override bool Equals(object obj)
		{
			if (obj is XTNumericToken)
				return this.ToObject().Equals(((XTNumericToken)obj).ToObject());
			return this.ToObject().Equals(obj);
		}

		public override int GetHashCode()
		{
			if (this is XTLongToken)
				return ((long)this).GetHashCode();
			return ((double)this).GetHashCode();
		}
		#endregion
	}

	// --------------------------------------------------------------
	// 长整型节点
	// --------------------------------------------------------------
	public class XTLongToken : XTNumericToken
	{
		private long m_value;

		public XTLongToken(long value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		public override long Round()
		{
			return this.m_value;
		}

		public override long Cut()
		{
			return this.m_value;
		}

		#region 与 long 互换

		// XTJsonLong 显式转换为 long
		public static explicit operator long(XTLongToken data)
		{
			return data.m_value;
		}

		// long 隐式转换为 XTJsonLong
		public static implicit operator XTLongToken(long value)
		{
			return new XTLongToken(value);
		}

		// -------------------------------------------
		// XTJsonLong 与 XTJsonLong 比较
		public static bool operator ==(XTLongToken v1, XTLongToken v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTLongToken v1, XTLongToken v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTLongToken v1, XTLongToken v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTLongToken v1, XTLongToken v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTJsonLong 与 long 比较
		public static bool operator ==(XTLongToken v1, long v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTLongToken v1, long v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTLongToken v1, long v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTLongToken v1, long v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// long 与 XTJsonLong 比较
		public static bool operator ==(long v1, XTLongToken v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(long v1, XTLongToken v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(long v1, XTLongToken v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(long v1, XTLongToken v2)
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
			if (obj is XTLongToken)
				return this.m_value.Equals(((XTLongToken)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}
		#endregion
	}

	// --------------------------------------------------------------
	// 浮点型节点
	// --------------------------------------------------------------
	public class XTDoubleToken : XTNumericToken
	{
		private double m_value;

		public XTDoubleToken(double value)
		{
			this.m_value = value;
		}

		public override object ToObject()
		{
			return this.m_value;
		}

		public override long Round()
		{
			return (long)(this.m_value + 0.5);
		}

		public override long Cut()
		{
			return (long)this.m_value;
		}

		#region 与 double 互换
		// XTDoubleToken 显式转换为 double
		public static explicit operator double(XTDoubleToken data)
		{
			return data.m_value;
		}

		// double 隐式转换为 XTDoubleToken
		public static implicit operator XTDoubleToken(double value)
		{
			return new XTDoubleToken(value);
		}

		// -------------------------------------------
		// 两个 XTDoubleToken 比较
		public static bool operator ==(XTDoubleToken v1, XTDoubleToken v2)
		{
			return v1.m_value == v2.m_value;
		}

		public static bool operator !=(XTDoubleToken v1, XTDoubleToken v2)
		{
			return v1.m_value != v2.m_value;
		}

		public static bool operator <(XTDoubleToken v1, XTDoubleToken v2)
		{
			return v1.m_value < v2.m_value;
		}

		public static bool operator >(XTDoubleToken v1, XTDoubleToken v2)
		{
			return v1.m_value > v2.m_value;
		}

		// -------------------------------------------
		// XTDoubleToken 与 double 比较
		public static bool operator ==(XTDoubleToken v1, double v2)
		{
			return v1.m_value == v2;
		}

		public static bool operator !=(XTDoubleToken v1, double v2)
		{
			return v1.m_value != v2;
		}

		public static bool operator <(XTDoubleToken v1, double v2)
		{
			return v1.m_value < v2;
		}

		public static bool operator >(XTDoubleToken v1, double v2)
		{
			return v1.m_value > v2;
		}

		// -------------------------------------------
		// double 与 XTDoubleToken 比较
		public static bool operator ==(double v1, XTDoubleToken v2)
		{
			return v1 == v2.m_value;
		}

		public static bool operator !=(double v1, XTDoubleToken v2)
		{
			return v1 != v2.m_value;
		}

		public static bool operator <(double v1, XTDoubleToken v2)
		{
			return v1 < v2.m_value;
		}

		public static bool operator >(double v1, XTDoubleToken v2)
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
			if (obj is XTDoubleToken)
				return this.m_value.Equals(((XTDoubleToken)obj).m_value);
			return this.m_value.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}
		#endregion
	}
}
