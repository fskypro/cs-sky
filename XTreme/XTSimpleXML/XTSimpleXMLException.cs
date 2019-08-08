// ------------------------------------------------------------------
// Description : XML 异常类
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2012.10.30
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace XTreme.XTSimpleXML
{
	// 异常基类
	public class SXMLException : XTException
	{
		protected string m_Msg;
		protected SXMLException()
		{
			this.Init(null);
		}

		virtual protected void Init(string msg)
		{
			this.m_Msg = msg;
		}

		override public string Message
		{
			get { return this.m_Msg; }
		}
	}

	// -----------------------------------------------------
	// 无效 xml 文件
	public class ErrorXMLException : SXMLException
	{
		public string FileName;
		public int LineNO;

		public ErrorXMLException(string fileName, int lineNO)
			: this(fileName, lineNO, null) { }

		public ErrorXMLException(string fileName, int lineNO, string msg)
			: base()
		{
			this.FileName = fileName;
			this.LineNO = lineNO;
			this.Init(msg);
		}

		override protected void Init(string msg)
		{
			string premsg = string.Format("Error xml file at line: {0}", this.LineNO);
			if (msg == null)
				msg = premsg + "!";
			else
				msg = string.Format("{0}\n\t{1}", premsg, msg);
			base.Init(msg);
		}
	}

	// 无效的 XML 标签
	public class InvalidTagXMLException : ErrorXMLException
	{
		public string Tag;
		public InvalidTagXMLException(string fileName, int lineNO, string tag)
			: base(fileName, lineNO)
		{
			this.Tag = tag;
			this.Init(string.Format("invalid tag: \"{0}\"", tag));
		}
	}

	// 没结束符“>”的 tag
	public class NoEndMarkXMLException : ErrorXMLException
	{
		public string Tag;
		public NoEndMarkXMLException(string fileName, int lineNO, string tag)
			: base(fileName, lineNO)
		{
			this.Tag = tag;
			this.Init(string.Format("tag {0} has no end mark '>'", tag));
		}
	}

	// 空 tag
	public class EmptyTagXMLExceptin : ErrorXMLException
	{
		public EmptyTagXMLExceptin(string fileName, int lineNO)
			: base(fileName, lineNO)
		{
			this.Init("contain an empty tag!");
		}
	}

	// tag 不匹配
	public class UnmatchTagXMLException : ErrorXMLException
	{
		public string StartTag;
		public string EndTag;
		public UnmatchTagXMLException(string fileName, int lineNO,
			string startTag, string endTag)
			: base(fileName, lineNO)
		{
			this.StartTag = startTag;
			this.EndTag = endTag;
			this.Init(string.Format("Tags do not match: \n\t{0}\n\t{1}",
				"start tag = " + StartTag, "end tag = " + endTag));
		}
	}

	// 没有终结 tag
	public class UnterminatedXMLException : ErrorXMLException
	{
		public string Tag;
		public UnterminatedXMLException(string fileName, int lineNO, string tag)
			: base(fileName, lineNO)
		{
			this.Tag = tag;
			this.Init(string.Format("{0} has no terminated tag!", tag));
		}
	}

	// 没有起始的 tag
	public class UnstartedXMLException : ErrorXMLException
	{
		public string Tag;
		public UnstartedXMLException(string fileName, int lineNO, string tag)
			: base(fileName, lineNO)
		{
			this.Tag = tag;
			this.Init(string.Format("Unstarted tag: {0}!", tag));
		}
	}

	// 无效的 XML 注释
	public class ErrorCommentXMLException : ErrorXMLException
	{
		public ErrorCommentXMLException(string fileName, int lineNO)
			: base(fileName, lineNO)
		{
			this.Init("error xml comment!");
		}
	}

	// -----------------------------------------------------
	// 错误的 tag 名称
	public class ErrorTagNameXMLException : SXMLException
	{
		public string TagName;
		public ErrorTagNameXMLException(string tagName)
		{
			this.TagName = tagName;
			this.Init(string.Format("Error xml tag name: {0}", tagName));
		}
	}

	// -----------------------------------------------------
	// 格式化标签值时错误
	public class FormatValueXMLException : SXMLException
	{
		public FormatValueXMLException(string strValue, string vtype)
		{
			this.Init(string.Format("Format error, can't convert {0} to value of {1}", strValue, vtype));
		}
	}

}
