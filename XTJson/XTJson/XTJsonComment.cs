// ------------------------------------------------------------------
// Description : JSON 注释
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Text;

namespace XTreme.XTJson
{
	public enum XTJsonCommentType { SingleLine, MultiLine }

	public class XTJsonComment
	{
		private XTJsonCommentType m_type;
		private string m_text;

		public XTJsonComment(XTJsonCommentType type, string text)
		{
			this.m_type = type;
			this.m_text = text;
		}

		public string Comment
		{
			get
			{
				if (this.m_type == XTJsonCommentType.SingleLine)
					return "// " + this.m_text;
				return string.Format("/*{0}*/", this.m_text);
			}
		}

		public override string ToString()
		{
			return this.m_text;
		}
	}
}
