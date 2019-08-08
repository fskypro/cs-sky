// ------------------------------------------------------------------
// Description : 实现字符串相关功能函数
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2012.10.30
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace XTreme.XTText
{
	public static class XTString
	{
		private static readonly Regex ReptnFirstWord = new Regex("^[^\\s\r\n]*\\s*");
		//private static readonly Regex ReptnLastWord = new Regex(@"(?<=\s)[^\s]*(\s+)?$");

		// -----------------------------------------------------------
		// 指出给定的字符串是否是纯数字字符串
		// -----------------------------------------------------------
		static public bool IsDigit(string str)
		{
			foreach (char c in str)
				if (c < '0' || c > '9')
					return false;
			return true;
		}

		// -----------------------------------------------------------
		// 生成单一 n 个字符组成的字符串
		// -----------------------------------------------------------
		static public string NChars(char c, int n)
		{
			char[] chs = new char[n];
			for (int i = 0; i < n; i++)
				chs[i] = c;
			return new string(chs);
		}

		// -----------------------------------------------------------
		// 获取一段文本中第一个单词
		// -----------------------------------------------------------
		static public string GetFirstWord(string text)
		{
			Match match = ReptnFirstWord.Match(text);
			if (match != null || match.Success)
				return match.Value;
			return "";
		}

		// -----------------------------------------------------------
		// 获取一段文本中第一个单词的结束位置
		// -----------------------------------------------------------
		static public int GetFirstWordEnd(string text)
		{
			Match match = ReptnFirstWord.Match(text);
			if (match != null || match.Success)
				return match.Length;
			return 0;
		}

		// -----------------------------------------------------------
		// 获取一段文本中最后一个单词
		// -----------------------------------------------------------
		static public string GetLastWord(string text)
		{
			Match match = ReptnFirstWord.Match(text);
			if (match != null || match.Success)
				return match.Value;
			return "";
		}

		// -----------------------------------------------------------
		// 获取一段文本中最后一个单词的起始位置
		// -----------------------------------------------------------
		static public int GetLastWorldStart(string text)
		{
			Match match = ReptnFirstWord.Match(text);
			if (match != null || match.Success)
				return match.Index;
			return 0;
		}
	}
}
