// ------------------------------------------------------------------
// Description : 数值解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;
using System.Globalization;

namespace XTreme.XTJson
{
	// --------------------------------------------------------------
	// 数值型解释器
	// --------------------------------------------------------------
	internal class XTJsonNumericParser
	{
		private static XTJsonData ParseHIntLong(XTJsonReader reader, bool isNegative)
		{
			int chr;
			StringBuilder nums = new StringBuilder();
			do
			{
				chr = reader.CurrChar();
				if (chr >= 48 && chr <= 57 || 
					chr >= 'A' && chr <= 'F' ||
					chr >= 'a' && chr <= 'f')
						reader.SkipChar();
				else
					break;
				nums.Append((char)chr);
			} while (chr > 0);


			string strValue = nums.ToString();
			if (strValue == "")
				reader.RaiseInvalidException();
			long value = long.Parse(strValue, NumberStyles.HexNumber);
			if (value > int.MinValue && value < int.MaxValue)
				return new XTJsonHexInt(isNegative ? -(int)value : (int)value);
			return new XTJsonHexLong(isNegative ? -value : value);
		}

		private static XTJsonData ParseDouble(XTJsonReader reader, string strInt, bool isNegative)
		{
			int chr;
			StringBuilder nums = new StringBuilder(strInt.ToString());
			nums.Append(".");
			do
			{
				chr = reader.CurrChar();
				if (chr >= 48 && chr <= 57)
					reader.SkipChar();
				else
					break;
				nums.Append((char)chr);
			} while (chr > 0);
			string strValue = nums.ToString();
			if (isNegative) strValue = "-" + strValue;
			return new XTJsonDouble(double.Parse(strValue));
		}


		// ----------------------------------------------------------
		private static XTJsonData Parse(XTJsonReader reader, int first, bool isNegative)
		{
			if (first == '.')
			{
				return ParseDouble(reader, "0", isNegative);
			}

			int chr;
			if(first == '0')
			{
				chr = reader.CurrChar();
				if (chr == 'x' || chr == 'X')
				{
					reader.SkipChar();
					return ParseHIntLong(reader, isNegative);
				}
			}

			// 浮点型或（长）整型
			StringBuilder nums = new StringBuilder(((char)first).ToString());
			do
			{
				chr = reader.CurrChar();
				if (chr >= 48 && chr <= 57)
					reader.SkipChar();
				else
					break;
				nums.Append((char)chr);
			} while (chr > 0);

			// 浮点型判断
			if (chr == '.')
			{
				reader.SkipChar();
				return ParseDouble(reader, nums.ToString(), isNegative);
			}

			// 整型
			long value = long.Parse(nums.ToString());
			if (value > int.MinValue && value < int.MaxValue)
				return new XTJsonInt(isNegative ? -(int)value : (int)value);
			return new XTJsonLong(isNegative ? -value : value);
		}

		public static XTJsonData Parse(XTJsonReader reader)
		{
			int chr = reader.CurrUnemptyChar();
			switch (chr)
			{
				case '-':
					reader.SkipChar();
					return Parse(reader, reader.NextChar(), true);
				case '+':
					reader.SkipChar();
					return Parse(reader, reader.NextChar(), false);
				case '.':
					reader.SkipChar();
					return ParseDouble(reader, "0", false);
				case '0':
					reader.SkipChar();
					chr = reader.CurrChar();
					if (chr == 'x' || chr == 'X')
					{
						reader.SkipChar();
						return ParseHIntLong(reader, false);
					}
					else
					{
						return Parse(reader, '0', false);
					}
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return Parse(reader, reader.NextChar(), false);
			}
			return null;
		}	
	}
}
