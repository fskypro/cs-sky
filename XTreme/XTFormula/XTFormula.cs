// ------------------------------------------------------------------
// Description : ��ʽ���ࣨ����Ƕ�׹�ʽ��
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
		private string m_formula;						// ��ʽ���ʽ
		private XTFormulaToken m_root;					// ��ʽ�﷨�����ڵ�
		private bool? m_currIsOpt;						// ��ʱ��ǵ�ǰ���͵��Ľڵ��ǲ��������
		private XTNumericToken m_result;				// Const ��ʽ������
		private readonly HashSet<string> m_argNames;	// ��ʽ��ʵ�ʰ��������в���

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
			get { return this.m_root != null ||		// ����ʽ
				this.m_currIsOpt == true; }			// ��������������﷨����
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
			if (this.m_currIsOpt == null)								// ��һ��Ԫ��
			{
				this.m_currIsOpt = false;
				if (optToken == null) return;							// ���������
				this.m_currIsOpt = true;
				if (optToken.AllowEmptyLToken) return;					// ������������������
				parser.RaiseFormulaException();							// ��˫Ŀ�������ͷ���﷨����
			}
			else if (this.m_currIsOpt == true)							// ǰһ��Ԫ���������
			{
				if (optToken != null)									// ����������һ�������
					parser.RaiseFormulaException();						// ����˫Ŀ�������һ���﷨����
				this.m_currIsOpt = false;
			}
			else														// ǰһ��Ԫ����ֵԪ��
			{
 				if (optToken == null)									// ��������һ��ֵԪ�أ��﷨����
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
			else if (this.m_root is XTOperatorToken)						// ���µ����﷨��
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
		// ������ʽ
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