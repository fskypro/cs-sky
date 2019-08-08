using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XTreme.XTPList
{
	public class XTPListFileException : Exception
	{
		public XTPListFileException(string msg) : base(msg) { }
	}

	// 无效的 PList 文件
	public class XTInvalidPListFileException : XTPListFileException
	{
		public XTInvalidPListFileException(string path)
			: base("Invalid plist file: " + path)
		{ }
	}

	// PList 绑定的图片资源不存在
	public class XTNotFoundPListImageFileException : XTPListFileException
	{
		public XTNotFoundPListImageFileException(string plistPath, string imgName)
			: base(string.Format("PList image is not exist: {0}/{1}",
			Path.GetDirectoryName(plistPath), imgName))
		{ }
	}

	// 不合法帧异常
	class XTFrameNotFoundException : XTPListFileException
	{
		public XTFrameNotFoundException(string frame)
			: base(string.Format("Frame '{0}' is not found.", frame))
		{ }
	}
}
