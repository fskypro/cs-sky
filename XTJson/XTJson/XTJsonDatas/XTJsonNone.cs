// ------------------------------------------------------------------
// Description : NULL JSON 数据
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
	internal class XTJsonNone: XTJsonData
	{
		private static XTJsonNone sm_inst = new XTJsonNone();

		public override XTJsonType Type { get { return XTJsonType.None; } }

		private XTJsonNone() { }

		public static XTJsonNone Inst { get { return sm_inst; } }

		public override object ToObject() { return null; }

		#region 与 null 互换
		// XTJsonNone 与 XTJsonNone 比较
		public static bool operator ==(XTJsonNone v1, XTJsonNone v2)
		{
			return true;
		}

		public static bool operator !=(XTJsonNone v1, XTJsonNone v2)
		{
			return false;
		}

		// XTJsonNone 与任何对象比较
		public static bool operator ==(XTJsonNone v1, object v2)
		{
			return v2 is XTJsonNone || v2 == null;
		}

		public static bool operator !=(XTJsonNone v1, object v2)
		{
			return (!(v2 is XTJsonNone)) && v2 != null;
		}

		// 任何对象与 XTJsonNone 比较
		public static bool operator ==(object v1, XTJsonNone v2)
		{
			return (v1 is XTJsonNone) || (v1 == null);
		}

		public static bool operator !=(object v1, XTJsonNone v2)
		{
			return (!(v1 is XTJsonNone)) && (v1 != null);
		}

		#endregion

		#region 模拟 null
		public override string ToString()
		{
			return "null";
		}

		public override bool Equals(object obj)
		{
			return obj == null || obj is XTJsonNone;
		}

		public override int GetHashCode()
		{
			return 0;
		}
		#endregion

	}
}
