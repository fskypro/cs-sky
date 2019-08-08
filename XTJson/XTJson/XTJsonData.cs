// ------------------------------------------------------------------
// Description : JSON 数据基类
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
	// JSON 类型定义
	// --------------------------------------------------------------
	public enum XTJsonType
	{
		None,
		Dict, List,
		String, Int, Long, HInt, HLong, Double, Bool
	}

	// --------------------------------------------------------------
	// JSON 数据基类
	// --------------------------------------------------------------
	public abstract class XTJsonData
	{
		public virtual XTJsonType Type { get{ return XTJsonType.None; } }
		public virtual object ToObject() { return null; }

		#region C# 内置类型转换为 XTJsonData
		public static implicit operator XTJsonData(bool value)
		{
			return new XTJsonBool(value);
		}

		public static implicit operator XTJsonData(Dictionary<XTJsonKeyable, XTJsonData> value)
		{
			// 迫使复制字典
			return new XTJsonDict((IDictionary<XTJsonKeyable, XTJsonData>)value);
		}

		public static implicit operator XTJsonData(double value)
		{
			return new XTJsonDouble(value);
		}

		public static implicit operator XTJsonData(float value)
		{
			return new XTJsonDouble(value);
		}

		public static implicit operator XTJsonData(short value)
		{
			return new XTJsonInt(value);
		}

		public static implicit operator XTJsonData(int value)
		{
			return new XTJsonInt(value);
		}

		public static implicit operator XTJsonData(List<XTJsonData> value)
		{
			// 迫使复制列表
			return new XTJsonList((IEnumerable<XTJsonData>)value);
		}

		public static implicit operator XTJsonData(XTJsonData[] datas)
		{
			return new XTJsonList(datas);
		}

		public static implicit operator XTJsonData(long value)
		{
			return new XTJsonLong(value);
		}

		public static implicit operator XTJsonData(string value)
		{
			return new XTJsonString(value);
		}
		#endregion

		#region XTJsonData 类型转换为 C# 内置类型
		public static explicit operator bool(XTJsonData data)
		{
			if (data.Type != XTJsonType.Bool)
				throw new XTJsonDataConvertException(data, typeof(bool));
			return (bool)(XTJsonBool)data;
		}

		public static explicit operator Dictionary<XTJsonKeyable, XTJsonData>(XTJsonData data)
		{
			if (data.Type != XTJsonType.Dict)
				throw new XTJsonDataConvertException(data, typeof(Dictionary<XTJsonKeyable, XTJsonData>));
			return (Dictionary<XTJsonKeyable, XTJsonData>)(XTJsonDict)data;
		}

		public static explicit operator float(XTJsonData data)
		{
			if (data is XTJsonNumeric)
				return Convert.ToSingle(data.ToObject());
			throw new XTJsonDataConvertException(data, typeof(double));
		}

		public static explicit operator double(XTJsonData data)
		{
			if (data is XTJsonNumeric)
				return Convert.ToDouble(data.ToObject());
			throw new XTJsonDataConvertException(data, typeof(double));
		}

		public static explicit operator short(XTJsonData data)
		{
			if (data is XTJsonNumeric)
				return Convert.ToInt16(data.ToObject());
			throw new XTJsonDataConvertException(data, typeof(int));
		}

		public static explicit operator int(XTJsonData data)
		{
			if (data is XTJsonNumeric)
				return Convert.ToInt32(data.ToObject());
			throw new XTJsonDataConvertException(data, typeof(int));
		}

		public static explicit operator List<XTJsonData>(XTJsonData data)
		{
			if (data.Type != XTJsonType.List)
				throw new XTJsonDataConvertException(data, typeof(List<XTJsonData>));
			return (List<XTJsonData>)(XTJsonList)data;
		}

		public static explicit operator XTJsonData[](XTJsonData data)
		{
			if (data.Type != XTJsonType.List)
				throw new XTJsonDataConvertException(data, typeof(XTJsonData[]));
			return (XTJsonData[])(XTJsonList)data;
		}

		public static explicit operator long(XTJsonData data)
		{
			if (data is XTJsonNumeric)
				return Convert.ToInt64(data.ToObject());
			throw new XTJsonDataConvertException(data, typeof(long));
		}

		public static explicit operator string(XTJsonData data)
		{
			if (data.Type != XTJsonType.String)
				throw new XTJsonDataConvertException(data, typeof(string));
			return (string)(XTJsonString)data;
		}

		#endregion
	}

	// --------------------------------------------------------------
	// JSON 单值类型（支持作为字典 key）
	// --------------------------------------------------------------
	public abstract class XTJsonKeyable : XTJsonData
	{
		public static implicit operator XTJsonKeyable(bool value)
		{
			return new XTJsonBool(value);
		}

		public static implicit operator XTJsonKeyable(double value)
		{
			return new XTJsonDouble(value);
		}

		public static implicit operator XTJsonKeyable(int value)
		{
			return new XTJsonInt(value);
		}

		public static implicit operator XTJsonKeyable(long value)
		{
			return new XTJsonLong(value);
		}

		public static implicit operator XTJsonKeyable(string value)
		{
			return new XTJsonString(value);
		}
	}

	// --------------------------------------------------------------
	// 数值类型
	// --------------------------------------------------------------
	public abstract class XTJsonNumeric : XTJsonKeyable
	{
 
	}
}
