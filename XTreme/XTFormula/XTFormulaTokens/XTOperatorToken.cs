// ------------------------------------------------------------------
// Description : 运算符
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
	internal class XTOperatorToken : XTFormulaToken
	{
		#region Dictionary<Operator, int> sm_pris
		private static Dictionary<Operator, int> sm_pris;	// 运算符优先级

		static XTOperatorToken()
		{
			sm_pris = new Dictionary<Operator, int>();
			sm_pris[Operator.Or]    = 0;
			sm_pris[Operator.Xor]   = 1;
			sm_pris[Operator.And]   = 2;
			sm_pris[Operator.LMove] = 3;
			sm_pris[Operator.RMove] = 3;
			sm_pris[Operator.Add]   = 4;
			sm_pris[Operator.Sub]   = 4;
			sm_pris[Operator.Mul]   = 5;
			sm_pris[Operator.Div]   = 5;
			sm_pris[Operator.Mod]   = 5;
			sm_pris[Operator.Pow]   = 6;
		}

		#endregion

		private Operator m_opt;
		private XTFormulaToken m_lToken;		// 左表达式
		private XTFormulaToken m_rToken;		// 右表达式

		public XTOperatorToken(Operator opt)
		{
			this.m_opt = opt;
			this.m_lToken = null;
			this.m_rToken = null;
		}

		public override string ToString()
		{
			return string.Format("XTOperatorToken({0})", this.m_opt.ToString());
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		private int Pri
		{
			get { return sm_pris[this.m_opt]; }
		}

		// 是否允许左子树为空
		public bool AllowEmptyLToken
		{
			get { return this.m_opt == Operator.Add || this.m_opt == Operator.Sub; }
		}

		// 是否为固定值
		public override bool IsConst
		{
			get 
			{
				if (this.m_lToken == null) return this.m_rToken.IsConst;
				return this.m_lToken.IsConst && this.m_rToken.IsConst;
			}
		}


		// ----------------------------------------------------------
		// private
		// ----------------------------------------------------------
		private XTFormulaToken AddValueToken(XTFormulaToken token)
		{
			XTOperatorToken rToken = this;
			while(true)
			{
				if (rToken.m_rToken is XTOperatorToken)
				{
					rToken = rToken.m_rToken as XTOperatorToken;
				}
				else
				{
					rToken.m_rToken = token;
					break;
				}
			}
			return this;
		}

		private XTFormulaToken AddOptToken(XTOperatorToken token)
		{
			if (token.Pri <= this.Pri)						// 新加的运算符，优先级低
			{
				token.m_lToken = this;						// 则将新运算符置顶
				return token;
			}

			XTOperatorToken rToken = this;
			while (true)
			{
				if ((rToken.m_rToken == null) ||
					!(rToken.m_rToken is XTOperatorToken) ||
					((XTOperatorToken)rToken.m_rToken).Pri >= token.Pri)
				{
					token.m_lToken = rToken.m_rToken;
					rToken.m_rToken = token;
					break;
				}
				rToken = rToken.m_rToken as XTOperatorToken;
			}
			return this;
		}

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		// 添加左节点
		public void SetLToken(XTFormulaToken token)
		{
			this.m_lToken = token;
		}

		// -------------------------------------------
		// 添加节点，并调整语法树
		public XTFormulaToken AddToken(XTFormulaToken token)
		{
			if (token is XTOperatorToken)
				return this.AddOptToken((XTOperatorToken)token);
			return this.AddValueToken(token);
		}


		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		public override XTNumericToken Calculate(string formula, XTFormulaArgs args)
		{
			XTNumericToken rValue = this.m_rToken.Calculate(formula, args);
			if (this.m_lToken == null)
			{	// 以 +、- 号开头的公式
				return (this.m_opt == Operator.Sub) ? -rValue : rValue;
			}
			XTNumericToken lValue = this.m_lToken.Calculate(formula, args);

			switch (this.m_opt)
			{
				case Operator.Or:
					return lValue | rValue;
				case Operator.Xor:
					return lValue ^ rValue;
				case Operator.And:
					return lValue & rValue;
				case Operator.LMove:
					return lValue << (int)rValue;
				case Operator.RMove:
					return lValue >> (int)rValue;
				case Operator.Add:
					return lValue + rValue;
				case Operator.Sub:
					return lValue - rValue;
				case Operator.Mul:
					return lValue * rValue;
				case Operator.Div:
					return lValue / rValue;
				case Operator.Mod:
					return lValue % rValue;
				case Operator.Pow:
					return Math.Pow((double)lValue, (double)rValue);
			}
			return rValue;		// 永远也不会走这里来
		}
	}
}
