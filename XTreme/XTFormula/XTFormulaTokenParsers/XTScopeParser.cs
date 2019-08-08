// ------------------------------------------------------------------
// Description : 优先单元元素
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	internal class XTScopeParser : XTFormulaTokenParser
	{
		public override XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			if (parser.CurrChar() != '(') return null;
			parser.NextChar();
			XTFormula formula = parser.InnerParse(argNames);
			int end = parser.NextChar();
			if (end != ')')
				parser.RaiseFormulaException();
			return new XTScopeToken(formula);
		}
	}
}
