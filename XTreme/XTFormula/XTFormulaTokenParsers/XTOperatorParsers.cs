// ------------------------------------------------------------------
// Description : 运算符元素基类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using OptItem = System.Collections.Generic.KeyValuePair<
	XTreme.XTFormula.Operator, string>;

namespace XTreme.XTFormula
{
	// --------------------------------------------------------------
	// base class
	// --------------------------------------------------------------
	internal class XTOperatorParser : XTFormulaTokenParser
	{
		private static List<OptItem> sm_opts;

		static XTOperatorParser()
		{
			sm_opts = new List<OptItem>(new OptItem[]{
				new OptItem(Operator.Pow, "**"),
				new OptItem(Operator.Mul, "*"),
				new OptItem(Operator.Div, "/"),
				new OptItem(Operator.Mod, "%"),
				new OptItem(Operator.Add, "+"),
				new OptItem(Operator.Sub, "-"),
				new OptItem(Operator.LMove, "<<"),
				new OptItem(Operator.RMove, ">>"),
				new OptItem(Operator.And, "&"),
				new OptItem(Operator.Xor, "^"),
				new OptItem(Operator.Or, "|"), });
		}

		public XTOperatorParser()
		{
		}

		// 解释表达式
		override public XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			foreach (OptItem item in sm_opts)
			{
				if (parser.NextString(item.Value))
					return new XTOperatorToken(item.Key);
			}
			return null;
		}
	}
}
