// ------------------------------------------------------------------
// Description : 值类型元素基类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NumericParser = System.Func<
	XTreme.XTFormula.XTFormulaParser,
	System.Collections.Generic.HashSet<string>,
	XTreme.XTFormula.XTFormulaToken>;

namespace XTreme.XTFormula
{
	// --------------------------------------------------------------
	// 值类型
	// --------------------------------------------------------------
	internal class XTNumericParser : XTFormulaTokenParser
	{
		private static Regex sm_reInt = new Regex(@"^[+-]?\d+\s*");
		private static Regex sm_reHInt = new Regex(@"^[+-]?0[xX][\da-fA-F]+\s*");
		private static Regex sm_reFloat = new Regex(@"^[+-]?\d*\.\d+\s*");
		private static NumericParser[] sm_parsers;

		static XTNumericParser()
		{
			// 注意，顺序不能改变
			sm_parsers = new NumericParser[] { 
				ParseHInt, 
				ParseFloat,
				ParseInt};
		}

		// 整型
		private static XTFormulaToken ParseInt(XTFormulaParser parser, HashSet<string> argNames)
		{
			Match m = parser.NextRegx(sm_reInt);
			if (m == null) return null;
			return new XTLongToken(long.Parse(m.Groups[0].Value));
		}

		// 十六进制整型
		private static XTFormulaToken ParseHInt(XTFormulaParser parser, HashSet<string> argNames)
		{
			Match m = parser.NextRegx(sm_reHInt);
			if (m == null) return null;
			long value = long.Parse(m.Groups[0].Value.Substring(2),
				System.Globalization.NumberStyles.HexNumber);
			return new XTLongToken(value);
		}

		// 浮点型
		private static XTFormulaToken ParseFloat(XTFormulaParser parser, HashSet<string> argNames)
		{
			Match m = parser.NextRegx(sm_reFloat);
			if (m == null) return null;
			double value = double.Parse(m.Groups[0].Value);
			return new XTDoubleToken(value);
		}

		public override XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			foreach (NumericParser func in sm_parsers)
			{
				XTFormulaToken token = func(parser, argNames);
				if (token == null) continue;
				return token;
			}
			return null;
		}
	}

}
