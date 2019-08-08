// ------------------------------------------------------------------
// Description : 列表解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;

namespace XTreme.XTJson
{
	internal class XTJsonListParser
	{
		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			if (chr != '[') return null;
			reader.SkipChar();
			List<XTJsonData> items = new List<XTJsonData>();
			XTJsonData item = null;
			do
			{
				chr = reader.CurrUnemptyChar();				// 尝试性探测下一个字符
				if (chr == ']')								// 字典结束
				{
					break;				
				}
				else if(chr == ',')							// 元素结束
				{
					reader.SkipChar();
					if (item == null)						// 错误：{,}
						reader.RaiseInvalidException();
					chr = reader.CurrUnemptyChar();
					if (chr == ',')							// 双逗号分隔错误：[xx, , yy}
						reader.RaiseInvalidException();
					if (chr == ']')							// 逗号加括号结束：[xx, yy, ]
						break;								// JSON 本身是不允许的，我这里允许
				}
				item = reader.ParsePart();
				if (item == null)
					reader.RaiseInvalidException();
				items.Add(item);
			} while (chr > 0);
			if (reader.NextUnemptyChar() != ']')
				reader.RaiseInvalidException();
			return new XTJsonList(items);
		}
	}
}
