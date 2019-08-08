// ------------------------------------------------------------------
// Description : 枚举附件属性
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------
// 使用方法：
// enum E
// {
//     [EnumStuffs("xxx")] A,
//     [EnumStuffs("xxx", "yyy")] B,
// }
// EnumStuffs.GetStuff(E.A)      // 返回“xxx”
// EnumStuffs.GetStuff(E.B, 1)   // 返回“yyy”
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XTreme.XTAttribute
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
	public class EnumStuffs : Attribute
	{
		private static Dictionary<string, Dictionary<string, EnumStuffs>> sm_estuffs;

		static EnumStuffs()
		{
			sm_estuffs = new Dictionary<string, Dictionary<string, EnumStuffs>>();
		}

		private object[] m_stuffs;

		public EnumStuffs(params object[] stuffs)
		{
			m_stuffs = stuffs;
		}

		public static object GetStuff(object evalue, int index = 0)
		{
			return GetStuffs(evalue)[index];
		}

		public static object[] GetStuffs(object evalue)
		{
			Dictionary<string, EnumStuffs> stuffs;
			Type etype = evalue.GetType();
			if (sm_estuffs.ContainsKey(etype.FullName))
			{
				stuffs = sm_estuffs[etype.FullName];
			}
			else
			{
				stuffs = new Dictionary<string, EnumStuffs>();
				sm_estuffs[etype.FullName] = stuffs;

				foreach (FieldInfo fi in etype.GetFields())
				{
					EnumStuffs[] eds = (EnumStuffs[])fi.GetCustomAttributes(typeof(EnumStuffs), false);
					if (eds.Length != 1) continue;
					stuffs[fi.Name] = eds[0];
				}
			}
			return stuffs[evalue.ToString()].m_stuffs;
		}
	}
}
