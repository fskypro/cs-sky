// ------------------------------------------------------------------
// Description : 字典解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using DictItem = System.Collections.Generic.KeyValuePair<
	XTreme.XTJson.XTJsonKeyable, XTreme.XTJson.XTJsonData>;
using Dict = System.Collections.Generic.Dictionary<
	XTreme.XTJson.XTJsonKeyable, XTreme.XTJson.XTJsonData>;

namespace XTreme.XTJson
{
	internal class XTJsonDictParser
	{
		private static DictItem ParseItem(XTJsonReader reader)
		{
			XTJsonData key = reader.ParsePart();
			if (!(key is XTJsonKeyable))				// 改数据不能作为字典的 key
				reader.RaiseInvalidException();
			int sp = reader.NextUnemptyChar();
			if (sp != ':')
				reader.RaiseInvalidException();
			XTJsonData value = reader.ParsePart();
			if (value == null)
				reader.RaiseInvalidException();
			return new DictItem((XTJsonKeyable)key, value);
		}
			
		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			if (chr != '{') return null;
			reader.SkipChar();							// 去掉左括号
			Dict dict = new Dict();
			bool isEmpty = true;
			DictItem item;
			do
			{
				chr = reader.CurrUnemptyChar();			// 尝试性探测下一个字符
				if (chr == '}')							// 字典结束
				{
					break;				
				}
				else if(chr == ',')						// 元素结束
				{
					reader.SkipChar();
					if (isEmpty)						// 错误：{,}
						reader.RaiseInvalidException();
					chr = reader.CurrUnemptyChar();
					if (chr == ',')						// 双逗号分隔错误：{xx: yy, , uu: vv}
						reader.RaiseInvalidException();
					if (chr == '}')						// 逗号加括号结束：{xx: yy, uu: vv, }
						break;							// JSON 本身是不允许的，我这里允许
				}
				item = ParseItem(reader);
				dict[item.Key] = item.Value;			// 允许添加重复键（dict.Add(...)，将不许添加重复键）
				isEmpty = false;
			}while(chr > 0);
			if (reader.NextUnemptyChar() != '}')
				reader.RaiseInvalidException();
			return new XTJsonDict(dict);
		}
	}
}
