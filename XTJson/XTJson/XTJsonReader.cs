// ------------------------------------------------------------------
// Description : JSON 数据解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace XTreme.XTJson
{
	internal class XTJsonReader
	{
		delegate XTJsonData Parser(XTJsonReader reader);
		private static List<Parser> sm_pasers = new List<Parser>();

		#region 构造函数

		static XTJsonReader()
		{
			sm_pasers.AddRange(new Parser[]{
				XTJsonStringParser.Parse,		// 字符串解释器
				XTJsonNumericParser.Parse,		// 数值型解释器
				XTJsonDictParser.Parse,			// 字典解释器
				XTJsonListParser.Parse,			// 列表解释器
				XTJsonBoolParser.Parse,			// 布尔型解释器
				XTJsonNoneParser.Parse,			// None 解释器
				});
		}

		// ----------------------------------------------------------
		private string m_path;
		private TextReader m_txtReader;
		private long m_pcurr;

		public XTJsonReader(TextReader tr)
			: this("", tr)
		{
		}

		public XTJsonReader(string path, TextReader tr)
		{
			this.m_path = path;
			this.m_txtReader = tr;
			this.m_pcurr = 0;
			if (this.CurrChar() == 65279)
				this.SkipChar();
		}

		#endregion

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		#region 错误处理
		// 检查是否为空 JSON 文件（JSON 串）
		private void EmptyCheck()
		{
			if (this.CurrUnemptyChar() <= 0)
			{
				if (this.m_path == "")
					throw new XTJsonEmptyException();
				throw new XTJsonEmptyException(this.m_path);
			}
		}

		// 抛出无效 JSON 异常
		public void RaiseInvalidException()
		{
			if (this.m_path == "")
				throw new XTJsonInvalidSourceException(this.m_pcurr);
			throw new XTJsonInvalidSourceException(this.m_path, this.m_pcurr);
		}

		#endregion

		#region 流操作封装
		// 获取当前解释到的非空字符
		public int CurrChar()
		{
			return this.m_txtReader.Peek();
		}

		// 调到下一个字符
		public void SkipChar()
		{
			this.m_pcurr += 1;
			this.m_txtReader.Read();
		}

		// 获取当前解释到的非空字符，并移动指针跳过空白字符
		public int CurrUnemptyChar()
		{
			int chr;
			while(true)
			{
				chr = this.m_txtReader.Peek();
				if (chr == ' ' || chr == '\t' || chr == '\r' || chr == '\n')
					this.m_txtReader.Read();
				else
					break;
			}
			return chr;
		}

		// 获取当前解释到的字符，并移动流指针
		public int NextChar()
		{
			this.m_pcurr += 1;
			return this.m_txtReader.Read();
		}

		// 获取当前解释到的非空白字符，并移动指针
		public int NextUnemptyChar()
		{
			int chr;
			do
			{
				chr = this.m_txtReader.Read();
				this.m_pcurr += 1;
			} while (chr == ' ' || chr == '\t' || chr == '\r' || chr == '\n');
			return chr;
		}

		// 获取当前解释到的一行，并移动指针
		public string NextLine()
		{
			string line = this.m_txtReader.ReadLine();
			this.m_pcurr += line.Length;
			return line;
		}

		// 读取一个块
		public string NextBlock(int len)
		{ 
			char[] buff = new char[len];
			len = this.m_txtReader.ReadBlock(buff, 0, len);
			this.m_pcurr += len;
			return new string(buff);
		}

		#endregion

		#region 内部解释接口
		public XTJsonData ParsePart()
		{
			XTJsonCommentParser.Parse(this);			// 中间的注释被忽略掉

			XTJsonData jdata;
			foreach (Parser parser in sm_pasers)
			{
				jdata = parser(this);
				if (jdata != null)
				{
					XTJsonCommentParser.Parse(this);	// 将后面的注释去掉
					return jdata;
				}
			}
			if (this.m_txtReader.Peek() <= 0)
				this.RaiseInvalidException();
			return null;
		}
		#endregion

		#region 解释入口
		public void Parse(out XTJsonDict jdict, List<XTJsonComment> doc=null)
		{
			this.EmptyCheck();
			XTJsonCommentParser.Parse(this, doc);
			XTJsonData dict = XTJsonDictParser.Parse(this);
			if (dict == null)
				this.RaiseInvalidException();
			jdict = (XTJsonDict)dict;
		}
		#endregion
	}
}
