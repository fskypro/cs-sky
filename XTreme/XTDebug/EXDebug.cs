// ------------------------------------------------------------------
// Description : 实现调试输出工具
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.19
// Histories   :
// ------------------------------------------------------------------

using System.Text;
using System.Diagnostics;

namespace XTreme.XTDebug
{
	public static class XTStackFrame
	{
		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		// 获取当前行号
		static public int __LINE__
		{
			get { return new StackTrace(1, true).GetFrame(0).GetFileLineNumber(); }
		}

		// 获取当前文件名
		static public string __FILE__
		{
			get { return new StackTrace(1, true).GetFrame(0).GetFileName(); }
		}

		// 获取当前函数名称
		static public string __FUNC__
		{
			get { return new System.Diagnostics.StackTrace(1, true).GetFrame(0).GetMethod().ToString(); }
		}


		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		// 获取调用栈帧
		static public StackFrame GetFrame(int depth=0)
		{
			return new StackTrace(1, true).GetFrame(depth);
		}
	}
}