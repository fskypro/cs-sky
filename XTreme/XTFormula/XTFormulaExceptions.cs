// ------------------------------------------------------------------
// Description : ��ʽ�쳣
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
	// ��ʽ���ʽ����
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
	// ���㹫ʽ����
	// --------------------------------------------------------------
	// ����ʱ�쳣
	public class XTFormulaCalculateException : XTFormulaException
	{
		public XTFormulaCalculateException(string err)
			: base(err)
		{
		}
	}

	// ������������빫ʽҪ��Ĳ���������һ�£����߲������Ͳ�������
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