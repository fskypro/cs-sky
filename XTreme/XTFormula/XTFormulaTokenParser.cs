// ------------------------------------------------------------------
// Description : ��ʽԪ�ػ���
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XTreme.XTFormula
{
	// --------------------------------------------------------------
	// ��ʽԪ�ػ���
	// --------------------------------------------------------------
	internal class XTFormulaTokenParser
	{
		protected XTFormulaTokenParser()
		{
		}

		public virtual XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			return null;
		}
	}
}