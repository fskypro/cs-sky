// ------------------------------------------------------------------
// Description : 公式节点
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------


using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	// --------------------------------------------------------------
	// 公式节点基类
	// --------------------------------------------------------------
	public abstract class XTFormulaToken
	{
		public virtual bool IsConst { get{ return true; } }
		public abstract XTNumericToken Calculate(string formula, XTFormulaArgs args);
	}
}
