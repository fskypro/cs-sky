// ------------------------------------------------------------------
// Description : 函数元素基类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	internal abstract class XTFuncToken : XTFormulaToken
	{
		protected XTFormula[] m_formulas;
		public XTFuncToken()
		{
		}

		public override bool IsConst
		{
			get
			{
				foreach (XTFormula formula in this.m_formulas)
					if (!formula.IsConst) return false;
				return true;
			}
		}

		public static XTFuncToken Create<T>(XTFormula[] formulas)
			where T : XTFuncToken, new()
		{
			XTFuncToken token = new T();
			token.m_formulas = formulas;
			return token;
		}
	}

	// --------------------------------------------------------------
	// 开方函数
	// --------------------------------------------------------------
	internal class ExSqrToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken baseToken = this.m_formulas[0].Calculate(args);
			XTNumericToken indexToken = this.m_formulas[1].Calculate(args);
			return new XTDoubleToken(Math.Pow((double)baseToken, 1.0f / (double)indexToken));
		}
	}

	// --------------------------------------------------------------
	// 取最大值函数
	// --------------------------------------------------------------
	internal class XTMaxToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken lToken = this.m_formulas[0].Calculate(args);
			XTNumericToken rToken = this.m_formulas[1].Calculate(args);
			if (lToken < rToken) return rToken;
			return lToken;
		}
	}

	// --------------------------------------------------------------
	// 取最小值函数
	// --------------------------------------------------------------
	internal class XTMinToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken lToken = this.m_formulas[0].Calculate(args);
			XTNumericToken rToken = this.m_formulas[1].Calculate(args);
			if (lToken > rToken) return rToken;
			return lToken;
		}
	}

	// --------------------------------------------------------------
	// 取随机值函数
	// --------------------------------------------------------------
	internal class XTRndToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken lToken = this.m_formulas[0].Calculate(args);
			XTNumericToken rToken = this.m_formulas[1].Calculate(args);
			return new Random().Next((int)lToken, (int)rToken);
		}
	}

	// --------------------------------------------------------------
	// 取整函数
	// --------------------------------------------------------------
	internal class XTIntPartToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken token = this.m_formulas[0].Calculate(args);
			return new XTLongToken(token.Cut());
		}
	}

	// --------------------------------------------------------------
	// 四舍五入函数
	// --------------------------------------------------------------
	internal class XTRoundToken : XTFuncToken
	{
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken token = this.m_formulas[0].Calculate(args);
			return new XTLongToken(token.Round());
		}
	}
}
