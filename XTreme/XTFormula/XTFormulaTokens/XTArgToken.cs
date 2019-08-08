// ------------------------------------------------------------------
// Description : 参数元素基类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	internal class XTArgToken : XTFormulaToken
	{
		private string m_name;

		public XTArgToken(string name)
		{
			this.m_name = name;
		}

		public string Name
		{
			get { return this.m_name; }
		}

		public override bool IsConst 
		{
			get { return false; }
		}

		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken token;
 			if(args.TryGetValue(this.m_name, out token)) return token;
			throw new XTFormulaNoArgumentException(formula, this.m_name);
		}
	}
}
