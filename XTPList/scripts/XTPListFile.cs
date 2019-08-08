// ------------------------------------------------------------------
// Description : PList XML 文件阅读器
// Author      : hyw
// Date        : 2014.11.26
// Histories   :
// ------------------------------------------------------------------

using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using PListElement = System.Collections.Generic.KeyValuePair<
	string, System.Xml.Linq.XElement>;

namespace XTreme.XTPList
{
	// ------------------------------------------------------------------------
	// XTPListXML
	// ------------------------------------------------------------------------
	#region XTPListXML
	static class XTPListXML
	{
		// 假设 “parent” 参数为 root，path 参数为 “first/second” 
		// 则调用结果返回 elem 标签：
		// <root> 
		//     <key> first </key> 
		//     <dict>
		//         <key> second </key>
		//         <elem> </elem>
		//     </dict>
		public static XElement GetSubElement(XElement parent, string path)
		{
			XElement element = null;
			string[] keys = path.Split('/');
			foreach (string key in keys)
			{
				var es = from e in parent.Elements("key") where e.Value == key select e;
				if (es.Count<XElement>() < 1) return null;
				element = (XElement)es.First<XElement>().NextNode;
				parent = element;
			}
			return element;
		}

		// 假设 “parent” 参数为 root，path 参数为 “first/second” 
		// 则调用结果返回 elem 标签中的 “value” 字符串：
		// <root> 
		//     <key> first </key> 
		//     <dict>
		//         <key> second </key>
		//         <elem> value </elem>
		//     </dict>
		public static string GetSubElementValue(XElement parent, string path)
		{
			XElement e = GetSubElement(parent, path);
			return (e == null) ? null : e.Value;
		}

		// 假设 “parent” 参数为 root，path 参数为 “first/second” 
		// 则调用结果返回 elem 标签本身的名字 “elem”：
		// <root> 
		//     <key> first </key> 
		//     <dict>
		//         <key> second </key>
		//         <elem> value </elem>
		//     </dict>
		public static string GetSubNodeName(XElement parent, string path)
		{
			XElement e = GetSubElement(parent, path);
			return (e == null) ? null : e.Name.ToString();
		}

		// 假设 “parent” 参数为 root，path 参数为 “first/seconds” 
		// 则调用结果返回 seconds/dict 标签下的所有嵌套标签迭代
		// 每个迭代为：KeyValuePair("item1", elem1 所在 dict 的 Element)
		// <root> 
		//     <key> first </key> 
		//     <dict>
		//         <key> seconds </key>
		//         <dict> 
		//             <key> item1 </key>
		//             <dict> elem1 </dict>
		//            
		//             <key> item2 </key>
		//             <dict> elem2 </dict>
		//             ...
		//         </dict>
		//     </dict>
		public static IEnumerable<PListElement> IterSubElements(XElement parent, string path)
		{
			XElement element = GetSubElement(parent, path);
			if (element != null)
			{
				var subs = from e in element.Elements("key") select e;
				foreach (XElement sub in subs)
				{
					if ((sub.NextNode is XElement))
						yield return new PListElement(sub.Value, (XElement)sub.NextNode);
				}
			}
		}

		// 获取 key 选项的数量
		public static int GetSubElementCount(XElement parent, string path)
		{
			XElement element = GetSubElement(parent, path);
			if (element == null) { return 0; }
			var subs = from e in element.Elements("key") select e;
			return subs.Count<XElement>();
		}

		// -------------------------------------------
		// 解释大小表达式：{x, y}
		public static Size ExplainSize(string strSize)
		{
			if (strSize == null) return new Size();
			Regex re = new Regex(@"\{\s*(\d+)\s*,\s*(\d+)\s*\}");
			Match m = re.Match(strSize);
			if (!m.Success) return new Size();
			return new Size(
				int.Parse(m.Groups[1].Value),
				int.Parse(m.Groups[2].Value));
		}

		// 解释矩形表达式：{x, y, w, h}
		public static Rectangle ExplainRect(string strRect)
		{
			if (strRect == null) return new Rectangle();
			string elem = @"\{\s*(\d+)\s*,\s*(\d+)\s*\}";
			Regex re = new Regex(string.Format(@"\{{{0},{0}\}}", elem));
			Match m = re.Match(strRect);
			if (!m.Success) return new Rectangle();
			return new Rectangle(
				int.Parse(m.Groups[1].Value),
				int.Parse(m.Groups[2].Value),
				int.Parse(m.Groups[3].Value),
				int.Parse(m.Groups[4].Value));
		}
	}

	#endregion

	// ------------------------------------------------------------------------
	// Frame
	// ------------------------------------------------------------------------
	#region XTFrame

	public class XTFrame
	{
		private string m_name;
		private XElement m_root;
		public XTFrame(string name, XElement root)
		{
			this.m_name = name;
			this.m_root = root;
		}

		protected XTFrame(XTFrame frame)
			: this(frame.m_name, frame.m_root)
		{ }

		// ----------------------------------------------------------
		// private
		// ----------------------------------------------------------
		private string GetStr(string key)
		{
			return XTPListXML.GetSubElementValue(this.m_root, key);
		}

		private Size GetSize(string key)
		{
			return XTPListXML.ExplainSize(XTPListXML.GetSubElementValue(this.m_root, key));
		}

		private Point GetPoint(string key)
		{
			Size size = this.GetSize(key);
			return new Point(size.Width, size.Height);
		}

		private Rectangle GetRect(string key)
		{
			return XTPListXML.ExplainRect(XTPListXML.GetSubElementValue(this.m_root, key));
		}

		private bool GetBool(string key)
		{
			return bool.Parse(XTPListXML.GetSubNodeName(this.m_root, key));
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		public string Name { get { return this.m_name; } }

		// sprite 在原图中的位置和大小
		public Rectangle Frame { get { return this.GetRect("frame"); } }

		// 在合成大图的过程中 texture-package 会吧原图透明的部分删除，这张新的图片与原图它们的中心点之间的向量。
		public Point Offset { get { return this.GetPoint("offset"); } }

		// 图片是否顺时针旋转90度
		public bool Rotated { get { return this.GetBool("rotated"); } }

		// 截取原图透明部分的坐标以及大小。
		public Rectangle SourceColorRect { get { return this.GetRect("sourceColorRect"); } }

		// 原图的大小。也是 sprite 的大小
		public Size SourceSize { get { return this.GetSize("sourceSize"); } }
	}

	#endregion

	// ------------------------------------------------------------------------
	// XTPListXMLFile
	// ------------------------------------------------------------------------
	#region XTPListXMLFile
	public class XTPListFile
	{
		#region 静态构造
		// 如果 Plist 文件不存在，则抛出 FileNotFoundException 异常
		// 如果不是合法的 PList 文件，则抛出 XTInvalidPListFileException 异常
		public static XTPListFile Create(string file, bool isCache = true)
		{
			XElement root;
			XDocument doc = XDocument.Load(file);
			try
			{
				root = doc.Element("plist").Element("dict");
				XTPListXML.GetSubElement(root, "frames");						// 检测是否有贴图帧
				XTPListXML.GetSubElement(root, "metadata/textureFileName");		// 检测贴图文件是否存在
			}
			catch
			{
				throw new XTInvalidPListFileException(file);
			}
			return new XTPListFile(file, root, isCache);
		}

		#endregion

		private string m_file;
		private XElement m_root;
		private Dictionary<string, XTFrame> m_frames;

		private XTPListFile(string file, XElement root, bool isCache)
		{
			this.m_file = file;
			this.m_root = root;
			if (isCache)
				this.m_frames = new Dictionary<string, XTFrame>();
			else
				this.m_frames = null;
		}


		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		public int Format
		{
			get { return int.Parse(XTPListXML.GetSubElementValue(this.m_root, "metadata/format")); }
		}

		public string FilePath
		{
			get { return this.m_file; }
		}

		public Size TextureSize
		{
			get { return XTPListXML.ExplainSize(
				XTPListXML.GetSubElementValue(this.m_root, "metadata/size"));
			}
		}

		public string TextureFileName
		{
			get { return XTPListXML.GetSubElementValue(this.m_root, "metadata/textureFileName"); }
		}

		public string TextureFilePath
		{
			get
			{
				string textureFileName = this.TextureFileName;
				if (textureFileName == null) return null;
				return Path.Combine(Path.GetDirectoryName(this.m_file), textureFileName); 
			}
		}

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		public XTFrame GetFrame(string name)
		{
			if (this.m_frames == null)
			{
				XElement e = XTPListXML.GetSubElement(this.m_root, "frames/" + name);
				if (e == null) return null;
				return new XTFrame(name, e);
			}
			XTFrame frame = null;
			if (!this.m_frames.TryGetValue(name, out  frame))
			{
				XElement e = XTPListXML.GetSubElement(this.m_root, "frames/" + name);
				if (e == null) return null;
				frame = new XTFrame(name, e);
				this.m_frames[name] = frame;
			}
			return frame;
		}

		public IEnumerable<XTFrame> IterFrames()
		{
			if (this.m_frames == null)
			{
				foreach (PListElement e in XTPListXML.IterSubElements(this.m_root, "frames"))
				{
					yield return new XTFrame(e.Key, e.Value);
				}
			}
			else
			{
				XTFrame frame;
				foreach (PListElement e in XTPListXML.IterSubElements(this.m_root, "frames"))
				{
					if (!this.m_frames.TryGetValue(e.Key, out frame))
					{
						frame = new XTFrame(e.Key, e.Value);
						this.m_frames[e.Key] = frame;
					}
					yield return frame;
				}
			}
		}

		public int GetFrameCount()
		{
			return XTPListXML.GetSubElementCount(this.m_root, "frames");
		}
	}
	#endregion
}
