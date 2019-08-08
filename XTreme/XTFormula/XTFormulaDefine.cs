// ------------------------------------------------------------------
// Description : 公式相关定义
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XTreme.XTFormula
{
	// 运算符定义，按优先级从低到高排序
	internal enum Operator
	{
		Or,					// |
		Xor,				// ^
		And,				// &
		LMove, RMove,		// <<、>>
		Add, Sub,			// +、-
		Mul, Div, Mod,		// *、/、%
		Pow,				// **
	}
}
