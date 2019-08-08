// ------------------------------------------------------------------
// Description : 注释解释器
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
	internal class XTJsonCommentParser
	{
		// 单行注释
		private static void ParseSingleLine(XTJsonReader reader, List<XTJsonComment> doc)
		{
			string line = reader.NextLine();
			if (doc != null)
				doc.Add(new XTJsonComment(XTJsonCommentType.SingleLine, line));
		}

		// 多行注释
		private static void ParseMultiLine(XTJsonReader reader, List<XTJsonComment> doc)
		{
			StringBuilder comment = new StringBuilder();
			int chr = reader.NextChar();
			bool hasEnd = false;
			while (chr > 0)
			{
				if (chr == '*')
				{
					if (reader.CurrChar() == '/')
					{
						reader.SkipChar();
						hasEnd = true;
						break;
					}
				}
				comment.Append((char)chr);
				chr = reader.NextChar();
			}
			if (!hasEnd)
				reader.RaiseInvalidException();			// JSON 格式错误，多行注释没有结束符
			if (doc != null)
				doc.Add(new XTJsonComment(XTJsonCommentType.MultiLine, comment.ToString()));
		}

		public static bool ParsePart(XTJsonReader reader, List<XTJsonComment> doc)
		{
			int first = reader.CurrUnemptyChar();
			if (first <= 0) return false;				// 文档结束
			if (first != '/') return false;				// 非注释区
			reader.SkipChar();

			int snd = reader.NextChar();				// 第二个字符
			if (snd == '/')								// 单行注释
			{
				ParseSingleLine(reader, doc);
			}
			else if (snd == '*')						// 多行注释
			{
				ParseMultiLine(reader, doc);
			}
			else
			{
				reader.RaiseInvalidException();			// JSON 格式错误
			}
			return true;
		}

		public static bool Parse(XTJsonReader reader, List<XTJsonComment> doc = null)
		{
			bool hasComment = false;
			while (ParsePart(reader, doc))
			{
				hasComment = true;
			}
			return hasComment;
		}
	}
}
