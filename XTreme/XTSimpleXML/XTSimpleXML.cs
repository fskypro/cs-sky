// ------------------------------------------------------------------
// Description : XML 入口
// Attention   : 该解释器效率不高，通常用于读取一些简单的系统配置，不要用于解释大文件
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2012.10.30
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XTreme.XTIO;

namespace XTreme.XTSimpleXML
{
	#region 所有标记解释正则
	internal struct RePtns
	{
		static public string ErrTagChars = "<>\\/\n\r";
		static public Regex ReptnErrTagName = new Regex(string.Format(
			@"[{0}]", ErrTagChars));									// 错误 tag 名称匹配器
		static public Regex ReptnStartTag = new Regex(string.Format(
			@"^<([^{0}]*)>([^<>]*)", ErrTagChars));						// 匹配起始 tag
		static public Regex ReptnEndTag = new Regex(
			@"^</([^\\<>\n\r]*)>\s*");									// 匹配结束 tag
		static public Regex ReptnOneTagScope = new Regex(string.Format(
			@"^<([^{0}]*)/\s*>\s*", ErrTagChars));						// 匹配空 Section：<XXXX/>
		static public Regex ReptnComment = new Regex(
			@"(?s)^<!--.*?-->\s*");										// 匹配注释 

		static public Regex ReptnNewline = new Regex("\r\n|\r|\n");		// 匹配换行
	}
	#endregion

	public class XTSimpleXML
	{
		static private Dictionary<string, XTSimpleXMLSection> sm_caches;	// XML 文件缓存

		static XTSimpleXML()
		{
			sm_caches = new Dictionary<string, XTSimpleXMLSection>();
		}


		#region XTSimpleXML 构造函数
		
		private string m_root;												// 根路径


		public XTSimpleXML() : this("") {}

		public XTSimpleXML(string root)
		{
			this.m_root = XTPath.NormalizePath(root);
		}

		#endregion

		#region private methods
		// -----------------------------------------------------------
		// private
		// -----------------------------------------------------------
		// 获取 XML 文本行索引
		static private int GetLineCount(string text, int pointer)
		{
			text = text.Substring(0, pointer);
			return RePtns.ReptnNewline.Matches(text).Count + 1;
		}


		#region 解释 XML 文件
		static private XTSimpleXMLSection Explain(string fileName, string text)
		{
			text = text.Trim();
			if (text == "")
			{
				string name = Path.GetFileName(fileName);
				return new XTSimpleXMLSection(name, 0, "", fileName);
			}
			if (!text.StartsWith("<"))
			{
				throw new ErrorXMLException(fileName, 1);
			}

			string orign = text;
			int pointer = 0;
			XTSimpleXMLSection root = null;
			Stack<XTSimpleXMLSection> sects = new Stack<XTSimpleXMLSection>();
			Match match;
			string tagName;
			while (true)
			{
				if (text == "") break;
				if (text.StartsWith("<!--"))
				{
					match = RePtns.ReptnComment.Match(text);
					if (match == null || !match.Success)
					{
						int lineNO = GetLineCount(orign, pointer);
						throw new ErrorCommentXMLException(fileName, lineNO);
					}
					pointer += match.Length;
					text = text.Substring(match.Length);
				}
				else if (text.StartsWith("</"))											// 结束标记
				{
					if (sects.Count == 0)												// 没有起始标记的结束标记
					{
						match = Regex.Match(text.Remove(0, 2),
							string.Format("^[^{0}]*", RePtns.ErrTagChars));				// 找出该没起始 tag 的结束 tag
						int lineNO = GetLineCount(orign, pointer);
						throw new UnstartedXMLException(fileName, lineNO, match.Value);
					}
					match = RePtns.ReptnEndTag.Match(text);
					if (!match.Success)													// 错误的结束标记
					{
						int lineNO = GetLineCount(orign, pointer);
						throw new UnterminatedXMLException(fileName, lineNO, sects.Peek().Name);
					}
					tagName = match.Groups[1].Value;
					if (tagName == sects.Peek().Name)									// 去掉一个嵌套
					{
						sects.Pop();
						int index = match.Index + match.Length;
						pointer += index;
						text = text.Substring(index, text.Length - index);
						if (sects.Count == 0) break;
					}
					else																// 结束 tag 与起始 tag 不一致
					{
						int lineNO = GetLineCount(orign, pointer);
						throw new UnmatchTagXMLException(fileName,
							lineNO, sects.Peek().Name, tagName);
					}
				}
				else if (text.StartsWith("<"))											// 起始标记
				{
					match = RePtns.ReptnStartTag.Match(text);
					if (match != null && match.Success)													// 寻找开始 tag
					{
						tagName = match.Groups[1].Value;
						string value = match.Groups[2].Value;
						if (tagName == "")												// 空 tag
						{
							int lineNO = GetLineCount(orign, pointer);
							throw new EmptyTagXMLExceptin(fileName, lineNO);
						}

						XTSimpleXMLSection sect;
						int layer = sects.Count;
						if (layer > 0)
						{
							sect = new XTSimpleXMLSection(tagName, layer, value);
							sects.Peek().AddSubSect(sect);
						}
						else
						{
							root = sect = new XTSimpleXMLSection(tagName, 0, value, fileName);
						}
						sects.Push(sect);
						int index = match.Index + match.Length;
						pointer += index;
						text = text.Substring(index, text.Length - index);
					}
					else															
					{
						match = RePtns.ReptnOneTagScope.Match(text);					// <XXXX/>
						if (match != null && match.Success)
						{
							tagName = match.Groups[1].Value;
							if (tagName == "")											// 空 tag
							{
								int lineNO = GetLineCount(orign, pointer);
								throw new EmptyTagXMLExceptin(fileName, lineNO);
							}
							XTSimpleXMLSection sect;
							int layer = sects.Count;
							if (layer > 0)
							{
								sect = new XTSimpleXMLSection(tagName, layer);
								sects.Peek().AddSubSect(sect);
							}
							else
							{
								root = sect = new XTSimpleXMLSection(tagName, 0, "", fileName);
							}
							int index = match.Index + match.Length;
							pointer += index;
							text = text.Substring(index, text.Length - index);
						}
						else															// 不是合法的 tag
						{
							match = Regex.Match(text.Remove(0, 1),
								string.Format("^[^{0}]*", RePtns.ErrTagChars)); 		// 找出该不合法的开始 tag
							int lineNO = GetLineCount(orign, pointer);
							tagName = match.Groups[1].Value;
							throw new NoEndMarkXMLException(fileName, lineNO, tagName);
						}
					}
				}
				else																	// 标记以外的内容错误
				{
					int lineNO = GetLineCount(orign, pointer);
					match = Regex.Match(text, "^[^\r\n]*");
					if (!match.Success)
						throw new ErrorXMLException(fileName, lineNO, "invalid xml file!");
					throw new InvalidTagXMLException(fileName, lineNO, match.Value);
				}
			}
			if (sects.Count > 0)
			{
				int lineNO = GetLineCount(orign, pointer);
				throw new UnterminatedXMLException(fileName, lineNO, sects.Peek().Name);
			}
			if (text != "")
			{
				int lineNO = GetLineCount(orign, pointer);
				throw new ErrorXMLException(fileName, lineNO);
			}
			return root;
		}

		#endregion

		#endregion

		#region public properties
		// -----------------------------------------------------------
		// properties
		// -----------------------------------------------------------
		public string Root
		{
			get { return this.m_root; }
		}

		#endregion

		#region public
		// -----------------------------------------------------------
		// public
		// -----------------------------------------------------------
		public XTSimpleXMLSection Open(string file)
		{
			return this.Open(file, false);
		}

		public XTSimpleXMLSection Open(string file, bool create)
		{
			return this.Open(file, create, Encoding.Default);
		}

		public XTSimpleXMLSection Open(string file, bool create, Encoding enc)
		{
			XTSimpleXMLSection sect = null;
			file = XTPath.NormalizePath(file);
			string fullPath = System.IO.Path.Combine(this.m_root, file);
			if (sm_caches.ContainsKey(fullPath))
				sect = sm_caches[fullPath];
			if (sect == null)
			{
				string text = "";
				if (File.Exists(fullPath))
				{
					StreamReader reader = new StreamReader(fullPath, enc);
					text = reader.ReadToEnd();
					reader.Close();
				}
				else if (create)
				{
					string path = Path.GetDirectoryName(fullPath);
					Directory.CreateDirectory(path);
				}
				else
				{
					throw new System.IO.FileNotFoundException(
						string.Format("xml file '{0}' is not exist!", file));
				}
				sect = Explain(fullPath, text);
				sm_caches[fullPath] = sect;
			}
			return sect;
		}

		public bool Purge(string file)
		{
			string path = XTPath.NormalizePath(file);
			//string fullPath = Path.Combine(path, file);
			return sm_caches.Remove(path);
		}

		#endregion
	}
}
