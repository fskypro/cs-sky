// ------------------------------------------------------------------
// Description : 实现时间相关功能函数
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2012.10.30
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace XTreme.XTUtilities
{
	public static class XTDateTime
	{
		static private DateTime sm_startTime = DateTime.Parse("1970-1-1");

		// 获取 1970 年以来的秒数
		static public double GetSeconds()
		{
			return (new TimeSpan(sm_startTime.Ticks - DateTime.Now.Ticks)).TotalSeconds;
		}

		// 获取 1970 年以来的毫秒数
		static public double GetMilliseconds()
		{
			return (new TimeSpan(sm_startTime.Ticks - DateTime.Now.Ticks)).TotalMilliseconds;
		}
	}
}
