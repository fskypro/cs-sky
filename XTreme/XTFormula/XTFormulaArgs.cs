// ------------------------------------------------------------------
// Description : 实现公式参数
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
	public delegate XTNumericToken XTFormulaArgGetter(params object[] args);

	#region XTFormulaArg
	public class XTFormulaArg
	{
		private XTNumericToken m_value;
		private XTFormulaArgGetter m_getter;
		private object[] m_getterArgs;

		#region 构造函数
		public XTFormulaArg(int v)
		{
			this.m_value = new XTLongToken(v);
			this.m_getter = null;
			this.m_getterArgs = null;
		}

		public XTFormulaArg(long v)
		{
			this.m_value = new XTLongToken(v);
			this.m_getter = null;
			this.m_getterArgs = null;
		}

		public XTFormulaArg(double v)
		{
			this.m_value = new XTDoubleToken(v);
			this.m_getter = null;
			this.m_getterArgs = null;
		}

		public XTFormulaArg(XTFormulaArgGetter getter, params object[] extras)
		{
			this.m_value = null;
			this.m_getter = getter;
			this.m_getterArgs = extras;
		}

		#endregion

		#region 数值型赋值
		public static implicit operator XTFormulaArg(int value)
		{
			return new XTFormulaArg(value);
		}

		public static implicit operator XTFormulaArg(double value)
		{
			return new XTFormulaArg(value);
		}

		public static implicit operator XTFormulaArg(long value)
		{
			return new XTFormulaArg(value);
		}
		#endregion

		public void ResetGetterArgs(params object[] extras)
		{
			this.m_getterArgs = extras;
		}

		public XTNumericToken Value
		{
			get 
			{
				if (this.m_getter == null) return this.m_value;
				return this.m_getter(this.m_getterArgs);
			}
		}
	}

	#endregion

	#region XTFormulaArgs
	public class XTFormulaArgs
	{
		private Dictionary<string, XTFormulaArg> m_args;

		public XTFormulaArgs()
		{
			this.m_args = new Dictionary<string, XTFormulaArg>();
		}

		public XTFormulaArgs(IDictionary<string, XTFormulaArg> args)
		{
			this.m_args = new Dictionary<string, XTFormulaArg>();
			foreach (KeyValuePair<string, XTFormulaArg> arg in args)
				this.m_args.Add(arg.Key, arg.Value);
		}

		public XTFormulaArg this[string key]
		{
			get 
			{
				return this.m_args[key];
			}
			set 
			{
				this.m_args[key] = value;
			}
		}

		public bool TryGetValue(string key, out XTNumericToken value)
		{
			XTFormulaArg arg;
			if (!this.m_args.TryGetValue(key, out arg))
			{
				value = null;
				return false;
			}
			value = arg.Value;
			return true;
		}

		public bool ContainsKey(string key)
		{
			return this.m_args.ContainsKey(key);
		}
	}

	#endregion
}
