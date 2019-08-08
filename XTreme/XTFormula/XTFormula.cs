// ------------------------------------------------------------------
// Description : 公式基类（包括嵌套公式）
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XTreme.XTFormula
{
	public class XTFormula
	{
		private string m_formula;						// 公式表达式
		private XTFormulaToken m_root;					// 公式语法树根节点
		private bool? m_currIsOpt;						// 临时标记当前解释到的节点是不是运算符
		private XTNumericToken m_result;				// Const 公式计算结果
		private readonly HashSet<string> m_argNames;	// 公式中实际包含的所有参数

		internal XTFormula(string formula, HashSet<string> argNames)
		{
			this.m_formula = formula;
			this.m_root = null;
			this.m_currIsOpt = null;
			this.m_result = null;
			this.m_argNames = argNames;
		}

		internal XTFormula(string formula)
			: this(formula, null)
		{
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", base.ToString(), this.m_formula);
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		public string Formula
		{
			get { return this.m_formula; }
		}

		internal bool IsVaild
		{
			get { return this.m_root != null ||		// 空算式
				this.m_currIsOpt == true; }			// 以运算符结束（语法错误）
		}

		public bool IsConst
		{
			get { return this.m_root.IsConst; }
		}

		public string[] Arguments
		{
			get 
			{
				if (this.m_argNames.Count == 0) return new string[0];
				int i = -1;
 				string[] argNames = new string[this.m_argNames.Count];
				foreach (string argName in this.m_argNames)
					argNames[++i] = argName;
				return argNames;
			}
		}

		// ----------------------------------------------------------
		// private
		// ----------------------------------------------------------
		private void Verify(XTFormulaParser parser, XTFormulaToken token)
		{
			XTOperatorToken optToken = token as XTOperatorToken;
			if (this.m_currIsOpt == null)								// 第一个元素
			{
				this.m_currIsOpt = false;
				if (optToken == null) return;							// 不是运算符
				this.m_currIsOpt = true;
				if (optToken.AllowEmptyLToken) return;					// 允许空左子树的运算符
				parser.RaiseFormulaException();							// 以双目运算符开头（语法错误）
			}
			else if (this.m_currIsOpt == true)							// 前一个元素是运算符
			{
				if (optToken != null)									// 紧接着又是一个运算符
					parser.RaiseFormulaException();						// 两个双目运算符连一起（语法错误）
				this.m_currIsOpt = false;
			}
			else														// 前一个元素是值元素
			{
 				if (optToken == null)									// 紧接着又一个值元素（语法错误）
					parser.RaiseFormulaException();
				this.m_currIsOpt = true;
			}
		}

		// ----------------------------------------------------------
		// internal
		// ----------------------------------------------------------
		internal virtual void AddToken(XTFormulaParser parser, XTFormulaToken token)
		{
			this.Verify(parser, token);

			XTOperatorToken optToken = token as XTOperatorToken;
			if (this.m_root == null)
			{
				this.m_root = token;
			}
			else if (this.m_root is XTOperatorToken)						// 重新调整语法树
			{
				this.m_root = ((XTOperatorToken)this.m_root).AddToken(token);
			}
			else if (optToken != null)
			{
				optToken.SetLToken(this.m_root);
				this.m_root = optToken;
			}
			else
			{
				parser.RaiseFormulaException();
			}
 		}

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		// 创建公式
		public static XTFormula Create(string formula, params string[] argNames)
		{
			return new XTFormulaParser(formula, argNames).RootParse();
		}

		public XTNumericToken Calculate()
		{
			return this.Calculate(new XTFormulaArgs());
		}

		public XTNumericToken Calculate(XTFormulaArgs args)
		{
			if (this.m_result != null) return this.m_result;

			XTNumericToken token = this.m_root.Calculate(this.m_formula, args);
			if (this.IsConst) this.m_result = token;
			return token;
		}
	}
}