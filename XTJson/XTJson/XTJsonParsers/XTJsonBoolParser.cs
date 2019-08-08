// ------------------------------------------------------------------
// Description : 布尔型解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;

namespace XTreme.XTJson
{
	internal class XTJsonBoolParser
	{
		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			if (chr == 't')
			{
				string text = reader.NextBlock(4);
				if (text == "true")
					return new XTJsonBool(true);
				reader.RaiseInvalidException();
			}
			else if (chr == 'f')
			{
				string text = reader.NextBlock(5);
				if (text == "false")
					return new XTJsonBool(false);
				reader.RaiseInvalidException();
			}
			return null;
		}
	}
}
