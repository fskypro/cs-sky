// ------------------------------------------------------------------
// Description : JSON 相关异常
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTreme.XTJson
{
	// --------------------------------------------------------------
	// XTJson 相关异常
	// --------------------------------------------------------------
	public class XTJsonException: Exception
	{
		public XTJsonException(string msg) : base(msg) { }
	}

	// --------------------------------------------------------------
	// 读取 JSON 文件错误
	// --------------------------------------------------------------
	public class XTJsonReadIOExcetion : XTJsonException
	{
		public XTJsonReadIOExcetion(Exception ex, string path)
			: base(string.Format("Read json file fail {0}\n:{1}", path, ex.Message))
		{ 
		}
	}

	// --------------------------------------------------------------
	// 解释 JSON 异常
	// --------------------------------------------------------------
	public class XTJsonParseException : XTJsonException
	{
		public XTJsonParseException(string msg) :
			base(msg) { }
	}

	// 空 JSON 文件（JSON 字符串）
	public class XTJsonEmptyException : XTJsonParseException
	{
		public XTJsonEmptyException()
			: base("Empty json stream.")
		{ }

		public XTJsonEmptyException(string path)
			: base("Empty json file: " + path)
		{ }
	}

	// 无效的 JSON 文件（JSON 串）
	public class XTJsonInvalidSourceException : XTJsonParseException
	{
		private long m_invalidPoint;

		public XTJsonInvalidSourceException(long invalidPoint)
			: base(string.Format("Invalid json stream(at point: {0})", invalidPoint))
		{
			this.m_invalidPoint = invalidPoint;
		}

		public XTJsonInvalidSourceException(string path, long invalidPoint)
			: base(string.Format("Invalid json file(at point: {0})", invalidPoint))
		{
			this.m_invalidPoint = invalidPoint;
		}

		public long InvalidPoint
		{
			get { return this.m_invalidPoint; }
		}
	}


	// --------------------------------------------------------------
	// 写入 JSON 文件错误
	// --------------------------------------------------------------
	public class XTJsonWriteIOExcetion : XTJsonException
	{
		public XTJsonWriteIOExcetion(Exception ex, string path)
			: base(string.Format("Write json file fail {0}\n:{1}", path, ex.Message))
		{
		}
	}

	// --------------------------------------------------------------
	// 无效的 JSON 数据
	// --------------------------------------------------------------
	public class XTJsonDataException : XTJsonException
	{
		public XTJsonDataException(object data)
			: base(string.Format("Invalid json data: {0}", data.ToString()))
		{ }
	}

	// --------------------------------------------------------------
	// JSON 字典 Key 为 null 异常
	// --------------------------------------------------------------
	public class XTJsonKeyNullException : XTJsonException
	{
		public XTJsonKeyNullException()
			: base(string.Format("Json key is not allow to be null."))
		{ }
	}

	// --------------------------------------------------------------
	// JSON 数据转换异常
	// --------------------------------------------------------------
	public class XTJsonDataConvertException : XTJsonException
	{
		public XTJsonDataConvertException(object value, Type type)
			: base(string.Format("Type {0} can't convert to {1}", value.GetType(), type.Name))
		{ }
	}


	// --------------------------------------------------------------
	// JSON 类型转换异常
	// --------------------------------------------------------------
	public class XTJsonTypeConvertException : XTJsonException
	{
		public XTJsonTypeConvertException(XTJsonType srcType, Type dstType)
		: base(string.Format("XTJsonType({0}) can't be converted to inner type {1}.", srcType.ToString(), dstType.Name))
		{ }

		public XTJsonTypeConvertException(Type srcType, Type dstType)
			: base(string.Format("XTJsonType({0}) can't be converted to inner type {1}.", srcType.Name, dstType.Name))
		{ }
	}
}
