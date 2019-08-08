// ------------------------------------------------------------------
// Description : NULL 解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;

namespace XTreme.XTJson
{
	internal class XTJsonNoneParser
	{
		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			if (chr != 'n') return null;
			string text = reader.NextBlock(4);
			if (text != "null")
				reader.RaiseInvalidException();
			return XTJsonNone.Inst;
		}
	}
}
