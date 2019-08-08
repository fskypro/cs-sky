// ------------------------------------------------------------------
// Description : 实现路径处理相关工具
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.19
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XTreme.XTIO
{
	public static class XTPath
	{
		// ----------------------------------------------------------
		// 正规化路径
		//   aa/bb\cc/../dd//ee 正规化后将变为：aa/bb/dd/ee
		// ----------------------------------------------------------
		public static string NormalizePath(string path, char splitter = '/')
		{
			string[] dirs = Regex.Split(path, @"/+|\\+");
			Stack<string> vdirs = new Stack<string>();
			foreach (string dir in dirs)
			{
				if (dir == "..")
				{
					if (vdirs.Count > 0 && vdirs.Peek() != "..")
						vdirs.Pop();
					else
						vdirs.Push(dir);
				}
				else if (dir != ".")
				{
					vdirs.Push(dir);
				}
			}
			if (vdirs.Count > 0)
				path = vdirs.Pop();
			while (vdirs.Count > 0)
			{
				path = vdirs.Pop() + splitter.ToString() + path;
			}
			return path;
		}
	}
}
