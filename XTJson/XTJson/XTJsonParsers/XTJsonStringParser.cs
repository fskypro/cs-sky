// ------------------------------------------------------------------
// Description : 字符串解释器
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
	internal class XTJsonStringParser
	{
		private static HashSet<int> sm_escs;

		static XTJsonStringParser()
		{
			sm_escs = new HashSet<int>(new int[]{
				'n', 'r', 't',
				'\'', '"', '\\', 
				'\0',				// 空字符
				'a',				// 鸣铃
				'b',				// 退格
				'f', 				// 走纸换页
				'v'});				// 竖向跳格
		}

		private static string TakeESC(XTJsonReader reader)
		{
			int chr = reader.NextChar();
			if (!sm_escs.Contains(chr))
				reader.RaiseInvalidException();
			return "\\" + (char)chr;
		}

		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			if (chr != '\"') return null;
			reader.SkipChar();
			StringBuilder sb = new StringBuilder();
			do
			{
				chr = reader.NextChar();
				if (chr == '\"')								// 字符串结束
					return new XTJsonString(sb.ToString());
				else if (chr == '\\')							// 转移符
					sb.Append(TakeESC(reader));
				else
					sb.Append((char)chr);
			} while (chr > 0);
			reader.RaiseInvalidException();
			return null;
		}
	}
}
