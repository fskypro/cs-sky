// ------------------------------------------------------------------
// Description : 实现编码转换
// Author      : hyw
// Date        : 2011.11.29
// Histories   :
// ------------------------------------------------------------------

using System.Text;

namespace XTreme.XTText
{
	static public class XTEncoding
	{
		/// <summary>
		/// 将字符串转换为字节数组
		/// </summary>
		/// <param name="text">要转换的字符串</param>
		/// <param name="srcEncoding">源编码</param>
		/// <param name="dstEncoding">目标编码</param>
		/// <returns>字节数组</returns>
		static public byte[] String2Bytes(string text, Encoding srcEncoding, Encoding dstEncoding)
		{
			byte[] buff = srcEncoding.GetBytes(text);
			return Encoding.Convert(srcEncoding, dstEncoding, buff);
		}

		/// <summary>
		/// 将字符串转换为字节数组
		/// </summary>
		/// <param name="text">要转换的文本</param>
		/// <param name="dstEncoding">目标编码</param>
		/// <returns>字节数组</returns>
		static public byte[] String2Bytes(string text, Encoding dstEncoding)
		{
			return String2Bytes(text, Encoding.Default, dstEncoding);
		}


		// -----------------------------------------------------------
		/// <summary>
		/// 将字节数组转换为字符串
		/// </summary>
		/// <param name="buff">要转换的字节数组</param>
		/// <param name="start">要转换字节数组的起始位置</param>
		/// <param name="count">要转换的字节个数</param>
		/// <param name="srcEncoding">字节数组编码</param>
		/// <param name="dstEncoding">字符串编码</param>
		/// <returns>转换后的字符串</returns>
		static public string Bytes2String(byte[] buff, int start, int count, Encoding srcEncoding, Encoding dstEncoding)
		{
			byte[] temp = Encoding.Convert(srcEncoding, dstEncoding, buff, start, count);
			return dstEncoding.GetString(temp);
		}

		/// <summary>
		/// 将字节数组转换为字符串
		/// </summary>
		/// <param name="buff">要转换的字节数组</param>
		/// <param name="srcEncoding">字节数组编码</param>
		/// <param name="dstEncoding">字符串编码</param>
		/// <returns>转换后的字符串</returns>
		static public string Bytes2String(byte[] buff, Encoding srcEncoding, Encoding dstEncoding)
		{
			byte[] temp = Encoding.Convert(srcEncoding, dstEncoding, buff);
			return dstEncoding.GetString(temp);
		}

		/// <summary>
		/// 将字节数组转换为字符串
		/// </summary>
		/// <param name="buff">要转换的字节数组</param>
		/// <param name="start">要转换字节数组的起始位置</param>
		/// <param name="count">要转换的字节个数</param>
		/// <param name="srcEncoding">字节数组编码</param>
		/// <returns>转换后的字符串</returns>
		static public string Bytes2String(byte[] buff, int start, int count, Encoding srcEncoding)
		{
			return Bytes2String(buff, start, count, srcEncoding, Encoding.Default);
		}

		/// <summary>
		/// 将字节数组转换为默认编码字符串
		/// </summary>
		/// <param name="buff">要转换的字节数组</param>
		/// <param name="srcEncoding">字节数组编码</param>
		/// <returns>转换后的字符串</returns>
		static public string Bytes2String(byte[] buff, Encoding srcEncoding)
		{
			return Bytes2String(buff, srcEncoding, Encoding.Default);
		}

		// -----------------------------------------------------------
		/// <summary>
		/// 转换字符串编码
		/// </summary>
		/// <param name="text">要转换的字符串</param>
		/// <param name="srcEncoding">源编码</param>
		/// <param name="dstEncoding">目标编码</param>
		/// <returns>转换后的字符串</returns>
		static public string ConverEncoding(string text, Encoding srcEncoding, Encoding dstEncoding)
		{
			byte[] buff = srcEncoding.GetBytes(text);
			Encoding.Convert(srcEncoding, dstEncoding, buff);
			return dstEncoding.GetString(buff);
		}
	}
}