// ------------------------------------------------------------------
// Description : JSON 数据扩展函数
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace XTreme.XTJson
{
	public static class XTJsonExtentions
	{
		// -------------------------------------------
		// 单值
		// -------------------------------------------
		public static int AsInt(this XTJsonData jdata)
		{
			try { return (int)jdata; }
			catch (Exception) { throw new XTJsonTypeConvertException(jdata.Type, typeof(int)); }
		}

		public static long AsLong(this XTJsonData jdata)
		{
			try { return (long)jdata; }
			catch (Exception) { throw new XTJsonTypeConvertException(jdata.Type, typeof(long)); }
		}

		public static float AsFloat(this XTJsonData jdata)
		{
			try { return (float)(double)jdata; }
			catch (Exception) { throw new XTJsonTypeConvertException(jdata.Type, typeof(float)); }
		}

		public static bool AsBool(this XTJsonData jdata)
		{
			try { return (bool)jdata; }
			catch (Exception) { throw new XTJsonTypeConvertException(jdata.Type, typeof(bool)); }
		}

		public static string AsString(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.String)
				return (string)jdata;
			return jdata.ToString();
		}

		// -------------------------------------------
		// 单类型列表
		// -------------------------------------------
		public static int[] AsInts(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.None)
				return null;
			XTJsonList jlist = jdata as XTJsonList;
			if (jlist == null)
				throw new XTJsonTypeConvertException(jdata.Type, typeof(int[]));
			int[] items = new int[jlist.Count];
			int index = -1;
			foreach (XTJsonData item in (XTJsonList)jdata)
			{
				try { items[++index] = (int)item; }
				catch { throw new XTJsonTypeConvertException(jdata.Type, typeof(int[])); }
			}
			return items;
		}

		public static long[] AsLongs(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.None)
				return null;
			XTJsonList jlist = jdata as XTJsonList;
			if (jlist == null)
				throw new XTJsonTypeConvertException(jdata.Type, typeof(long[]));
			long[] items = new long[jlist.Count];
			int index = -1;
			foreach (XTJsonData item in (XTJsonList)jdata)
			{
				try { items[++index] = (long)item; }
				catch { throw new XTJsonTypeConvertException(jdata.Type, typeof(long[])); }
			}
			return items;
		}

		public static float[] AsFloats(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.None)
				return null;
			XTJsonList jlist = jdata as XTJsonList;
			if (jlist == null)
				throw new XTJsonTypeConvertException(jdata.Type, typeof(float[]));
			float[] items = new float[jlist.Count];
			int index = -1;
			foreach (XTJsonData item in (XTJsonList)jdata)
			{
				try { items[++index] = (float)item; }
				catch { throw new XTJsonTypeConvertException(jdata.Type, typeof(float[])); }
			}
			return items;
		}

		public static bool[] AsBools(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.None)
				return null;
			XTJsonList jlist = jdata as XTJsonList;
			if (jlist == null)
				throw new XTJsonTypeConvertException(jdata.Type, typeof(bool[]));
			bool[] items = new bool[jlist.Count];
			int index = -1;
			foreach (XTJsonData item in (XTJsonList)jdata)
			{
				try { items[++index] = (bool)item; }
				catch { throw new XTJsonTypeConvertException(jdata.Type, typeof(bool[])); }
			}
			return items;
		}

		public static string[] AsStrings(this XTJsonData jdata)
		{
			if (jdata.Type == XTJsonType.None)
				return null;
			XTJsonList jlist = jdata as XTJsonList;
			if (jlist == null)
				throw new XTJsonTypeConvertException(jdata.Type, typeof(string[]));
			string[] items = new string[jlist.Count];
			int index = -1;
			foreach (XTJsonData item in (XTJsonList)jdata)
			{
				try { items[++index] = (string)item; }
				catch { throw new XTJsonTypeConvertException(jdata.Type, typeof(string[])); }
			}
			return items;
		}
	}
}
