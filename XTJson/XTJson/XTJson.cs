// ------------------------------------------------------------------
// Description : JSON 入口
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace XTreme.XTJson
{
	public static class XTJson
	{
		private static Dictionary<string, XTJsonRoot> sm_caches;

		static XTJson()
		{
			sm_caches = new Dictionary<string, XTJsonRoot>();
		}

		#region 写出 JSON
		// ----------------------------------------------------------
		// 以 UTF8 的格式保存一个 JSON 文件
		// 参数：
		//     path   : 要保存的路径
		//     jdict  : JSON 字典
		//     warps  : 表示字典的自动换行层级数
		//     doc    : JSON 文件的开始注释文档（不需要带注释符号）
		//     isCache: 是否放到缓存
		// 异常：
		//     XTJsonWriteIOExcetion: XTJsonException
		//     XTJsonDataException: XTJsonException
		// ----------------------------------------------------------
		internal static void Write(string path, XTJsonDict jdict,
			uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			Write(path, jdict, Encoding.UTF8, warps, doc, isCache);
		}

		// ----------------------------------------------------------
		// 保存一个 JSON 文件
		// 参数：
		//     path   : 要保存的路径
		//     jdict  : JSON 字典
		//     enc    : 写出 JSON 文件的编码格式
		//     warps  : 表示字典的自动换行层级数
		//     doc    : JSON 文件的开始注释文档（不需要带注释符号）
		//     isCache: 是否放到缓存
		// 异常：
		//     XTJsonWriteIOExcetion: XTJsonException
		//     XTJsonDataException: XTJsonException
		// ----------------------------------------------------------
		internal static void Write(string path, XTJsonDict jdict, Encoding enc,
			uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			FileStream fs = null;
			StreamWriter sw = null;
			try
			{
				fs = new FileStream(path, FileMode.Create);
				sw = new StreamWriter(fs, enc);
				(new XTJsonWriter(sw, warps)).Write(jdict, doc);
			}
			catch (XTJsonException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new XTJsonWriteIOExcetion(ex, path);
			}
			finally
			{
				if (sw != null) sw.Close();
				if (fs != null) fs.Close();
			}
			if (isCache)
			{
				Purge(path);
				sm_caches[ExPath.NormalizePath(path)] = new XTJsonRoot(path, jdict, enc, doc);
			}
		}

		#endregion

		#region 打开 JSON
		// ----------------------------------------------------------
		// 指定编码打开一个 JSON 文件
		// 参数：
		//     path    : 打开的 JSON 文件路径
		//     enc     : JSON 文件编码方式
		//     ignorDoc: 是否忽略配置前的注释文档，如果忽略注释文档，则下次保存时，文档将会丢失
		//     isCache : 是否作缓存处理（如果为 true，则下次打开时，不需要解释）
		//               如果要去掉缓存，调用 Purge()
		// 异常：
		//     XTJsonReadIOExcetion
		//     XTJsonEmptyException
		//     XTJsonInvalidSourceException
		// ----------------------------------------------------------
		public static XTJsonRoot Open(string path, Encoding enc, bool ignorDoc = true, bool isCache = true)
		{
			path = ExPath.NormalizePath(path);
			if (sm_caches.ContainsKey(path))
				return sm_caches[path];

			XTJsonDict jdict = null;
			List<XTJsonComment> doc = null;
			if (!ignorDoc) doc = new List<XTJsonComment>();
			FileStream fs = null; 
			StreamReader sr = null;
			try
			{
				fs = new FileStream(path, FileMode.OpenOrCreate);
				sr = new StreamReader(fs, enc);
			}
			catch (Exception ex)
			{
				throw new XTJsonReadIOExcetion(ex, path);
			}
			try
			{
				new XTJsonReader(path, sr).Parse(out jdict, doc);
			}
			catch (XTJsonEmptyException)
			{
				return new XTJsonRoot(path, enc);
			}
			catch (XTJsonParseException)
			{
				throw;
			}
			finally
			{
				if (sr != null) sr.Close();
				if (fs != null) fs.Close();
			}
			XTJsonRoot jroot = new XTJsonRoot(path, jdict, enc, doc);
			if (isCache) sm_caches[path] = jroot;
			return jroot;
		}

		// ----------------------------------------------------------
		// 以 UTF8 编码打开一个 JSON 文件
		// 参数：
		//     path    : 打开的 JSON 文件路径
		//     ignorDoc: 是否忽略配置前的注释文档，如果忽略注释文档，则下次保存时，文档将会丢失
		//     isCache : 是否作缓存处理（如果为 true，则下次打开时，不需要解释）
		//               如果要去掉缓存，调用 Purge()
		// 异常：
		//     XTJsonReadIOExcetion: XTJsonException
		//     XTJsonEmptyException: XTJsonParseException: XTJsonException
		//     XTJsonInvalidSourceException: XTJsonParseException: XTJsonException
		// ----------------------------------------------------------
		public static XTJsonRoot Open(string path, bool ignorDoc = true, bool isCache = true)
		{
			return Open(path, Encoding.UTF8, ignorDoc, isCache);
		}

		// ----------------------------------------------------------
		// 解释一个字符串形式的 JSON 数据
		// 异常：
		//     XTJsonEmptyException: XTJsonParseException: XTJsonException
		//     XTJsonInvalidSourceException: XTJsonParseException: XTJsonException
		// ----------------------------------------------------------
		public static XTJsonDict Explain(string jstr)
		{
			XTJsonDict jdict;
			StringReader sr = new StringReader(jstr);
			try
			{
				(new XTJsonReader(sr)).Parse(out jdict);
				return jdict;
			}
			catch (XTJsonParseException)
			{
				throw;
			}
			finally
			{
				sr.Close();
			}
		}


		// ----------------------------------------------------------
		// 解释一个流式的 JSON 数据
		// 异常：
		//     XTJsonEmptyException: XTJsonParseException: XTJsonException
		//     XTJsonInvalidSourceException: XTJsonParseException: XTJsonException
		// ----------------------------------------------------------
		public static XTJsonDict ExplainStream(StreamReader jstream, bool isEndClose=true)
		{
			XTJsonDict jdict;
			try
			{
				(new XTJsonReader(jstream)).Parse(out jdict);
				return jdict;
			}
			catch (XTJsonParseException)
			{
				throw;
			}
			finally
			{
				if (isEndClose)
					jstream.Close();
			}
		}

		// ----------------------------------------------------------
		// 施放被打开的 JSON 文件(只清除缓存中的数据，使得下次打开时，重新解释)
		// ----------------------------------------------------------
		public static bool Purge(string path)
		{ 
			path = ExPath.NormalizePath(path);
			return sm_caches.Remove(path);
		}

		#endregion
	}
}
