// ------------------------------------------------------------------
// Description : 公式异常
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	public class XTFormulaException : XTException
	{
		protected XTFormulaException(string err)
			: base(err)
		{ }
	}

	// --------------------------------------------------------------
	// 公式表达式错误
	// --------------------------------------------------------------
	public class XTFormulaErrorException : XTFormulaException
	{
		private string m_formula;
		private int m_point;
		public XTFormulaErrorException(string formula, int point)
			: base(string.Format("Error formula: \"{0}\" at point {1}", formula, point))
		{
			this.m_formula = formula;
			this.m_point = point;
		}

		public string Formula
		{
			get { return this.m_formula; }
		}

		public int ErrorPoint
		{
			get { return this.m_point; }
		}
	}

	// --------------------------------------------------------------
	// 计算公式错误
	// --------------------------------------------------------------
	// 运算时异常
	public class XTFormulaCalculateException : XTFormulaException
	{
		public XTFormulaCalculateException(string err)
			: base(err)
		{
		}
	}

	// 传入参数个数与公式要求的参数个数不一致，或者参数类型不可运算
	public class XTFormulaNoArgumentException : XTFormulaCalculateException
	{
		private string m_formula;
		private string m_argName;

		public XTFormulaNoArgumentException(string formula, string argName)
			: base(string.Format("No argument '{0}' value for formula calling.", argName))
		{
			this.m_formula = formula;
			this.m_argName = argName;
		}

		public string Formula
		{
			get { return this.m_formula; }
		}

		public string ArgName
		{
			get { return this.m_argName; }
		}
	}

}