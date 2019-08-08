// ------------------------------------------------------------------
// Description : 括号元素基类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	internal class XTScopeToken : XTFormulaToken
	{
		private XTFormula m_formula;

		public XTScopeToken(XTFormula formula)
		{
			this.m_formula = formula;
		}

		public override bool IsConst
		{
			get { return this.m_formula.IsConst; }
		}

		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			return this.m_formula.Calculate(args);
		}
	}
}
