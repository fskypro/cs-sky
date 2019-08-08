// ------------------------------------------------------------------
// Description : XML 标签类
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
using XTreme.XTText;

namespace XTreme.XTSimpleXML
{
	public delegate int XTSimpleXMLValueFilter<T>(XTSimpleXMLSection sect, out T value);

	public class XTSimpleXMLSection
	{
		static private Dictionary<string, string> sm_tagESCs;				// tag 标记转义符

		static XTSimpleXMLSection()
		{
			sm_tagESCs = new Dictionary<string, string>();
			sm_tagESCs.Add("<", "&lt;");
			sm_tagESCs.Add(">", "&gt;");
			sm_tagESCs.Add(" ", "&#x20;");
			sm_tagESCs.Add("\t", "&#x09;");
			sm_tagESCs.Add("\n", "&#x0a;");
			sm_tagESCs.Add("\r", "&#x0d;");
		}

		# region 构造函数

		private string m_Name;
		private int m_Layer;
		private string m_Value;
		private string m_FileName;
		private List<XTSimpleXMLSection> m_SubSects;

		public XTSimpleXMLSection(string name, int layer)
			: this(name, layer, "", "")
		{
		}

		public XTSimpleXMLSection(string name, int layer, string value)
			: this(name, layer, value, "")
		{
		}

		public XTSimpleXMLSection(string name, int layer, string value, string fileName)
		{
			this.m_Name = name;
			this.m_Layer = layer;
			this.m_Value = value.Trim();
			this.m_FileName = fileName;
			this.m_SubSects = new List<XTSimpleXMLSection>();
		}

		#endregion

		// ----------------------------------------------------------------
		// inner methods
		// ----------------------------------------------------------------
		public XTSimpleXMLSection this[string key]
		{
			get
			{
				Queue<string> keys = new Queue<string>(
					key.Split(new char[] { '/' }));
				XTSimpleXMLSection sect = this;
				while (keys.Count > 0)
				{
					key = keys.Dequeue();
					if (key == "") continue;
					bool finded = false;
					foreach (XTSimpleXMLSection s in sect.m_SubSects)
					{
						if (s.m_Name == key)
						{
							sect = s;
							finded = true;
							break;
						}
					}
					if (!finded)
						return null;
				}
				return sect;
			}
		}

		// ----------------------------------------------------------------
		// properties
		// ----------------------------------------------------------------
		public string Name
		{
			get { return this.m_Name; }
		}

		public int Layer
		{
			get { return this.m_Layer; }
		}

		// -------------------------------------------------
		public string AsString
		{
			get
			{
				string value = this.m_Value;
				foreach (string tag in sm_tagESCs.Keys)
					value = value.Replace(sm_tagESCs[tag], tag);
				return value;
			}
			set
			{
				foreach (string tag in sm_tagESCs.Keys)
					value = value.Replace(tag, sm_tagESCs[tag]);
				this.m_Value = value;
			}
		}

		public int AsInt
		{
			get { return ToInt(this.m_Value); }
			set { this.m_Value = value.ToString(); }
		}

		public long AsLong
		{
			get { return ToLong(this.m_Value); }
			set { this.m_Value = value.ToString(); }
		}

		public float AsFloat
		{
			get { return ToFloat(this.m_Value); }
			set { this.m_Value = value.ToString(); }
		}

		public double AsDouble
		{
			get { return ToDouble(this.m_Value); }
			set { this.m_Value = value.ToString(); }
		}

		public bool AsBool
		{
			get { return ToBool(this.m_Value); }
			set { this.m_Value = value.ToString(); }
		}

		public float[] AsVector2
		{
			get
			{
				return ToVector(this.m_Value, 2);
			}
			set
			{
				if (value.Length != 2)
					throw new RankException("Expected tuple with 2 float elements.");
				string[] strValue = new string[2];
				for (int i = 0; i < 2; ++i)
					strValue[i] = value[i].ToString();
				this.m_Value = string.Join(" ", strValue);
			}
		}

		public float[] AsVector3
		{
			get
			{
				return ToVector(this.m_Value, 3);
			}
			set
			{
				if (value.Length != 3)
					throw new RankException("Expected tuple with 3 float elements.");
				string[] strValue = new string[3];
				for (int i = 0; i < 3; ++i)
					strValue[i] = value[i].ToString();
				this.m_Value = string.Join(" ", strValue);
			}
		}

		public float[] AsVector4
		{
			get
			{
				return ToVector(this.m_Value, 4);
			}
			set
			{
				if (value.Length != 4)
					throw new RankException("Expected tuple with 4 float elements.");
				string[] strValue = new string[4];
				for (int i = 0; i < 4; ++i)
					strValue[i] = value[i].ToString();
				this.m_Value = string.Join(" ", strValue);
			}
		}


		// ----------------------------------------------------------------
		// private
		// ----------------------------------------------------------------
		static private int ToInt(string text)
		{
			if (text == "" || text == null) return 0;
			try { return int.Parse(text); }
			catch { throw new FormatValueXMLException(text, "int"); }
		}

		static private long ToLong(string text)
		{
			if (text == "" || text == null) return 0L;
			try { return long.Parse(text); }
			catch { throw new FormatValueXMLException(text, "long"); }
		}

		static private float ToFloat(string text)
		{
			if (text == "" || text == null) return 0.0f;
			try { return float.Parse(text); }
			catch { throw new FormatValueXMLException(text, "float"); }
		}

		static private double ToDouble(string text)
		{
			if (text == "" || text == null) return 0.0;
			try { return double.Parse(text); }
			catch { throw new FormatValueXMLException(text, "double"); }
		}

		static private bool ToBool(string text)
		{
			if (text == "" || text == null) return false;
			if (text.ToLower() == "true") return true;
			if (text.ToLower() == "false") return false;
			if (XTString.IsDigit(text)) return int.Parse(text) != 0;
			try { return float.Parse(text) != 0; }
			catch { return false; }
		}

		static private float[] ToVector(string text, int unit)
		{
			if (text == null)
				return new float[] { 0.0f, 0.0f };
			string[] elems = Regex.Split(text, @"\s+");
			if (elems.Length != unit)
				throw new FormatValueXMLException(text, "Vector" + unit.ToString());
			float[] vector = new float[unit];
			for (int i = 0; i < unit; ++i)
				try { vector[i] = float.Parse(elems[i]); }
				catch { throw new FormatValueXMLException(text, "Vector" + unit); }
			return vector;
		}

		// -------------------------------------------------------
		private void WriteXMLCode(StreamWriter writer)
		{
			if (this.m_SubSects.Count > 0)
			{
				if (this.m_Value == "")
					writer.Write(string.Format("{0}<{1}>\n",
						XTString.NChars('\t', this.m_Layer), this.m_Name));
				else
					writer.Write(string.Format("{0}<{1}>\t{2}\n",
						XTString.NChars('\t', this.m_Layer), this.m_Name, this.m_Value));
				foreach (XTSimpleXMLSection sect in this.m_SubSects)
					sect.WriteXMLCode(writer);
				writer.Write(string.Format("{0}</{1}>\n",
					XTString.NChars('\t', this.m_Layer), this.m_Name));
			}
			else
			{
				if (this.m_Value == "")
					writer.Write(string.Format("{0}<{1}>\t",
						XTString.NChars('\t', this.m_Layer), this.m_Name));
				else
					writer.Write(string.Format("{0}<{1}>\t{2}\t",
						XTString.NChars('\t', this.m_Layer), this.m_Name, this.m_Value));
				writer.Write(string.Format("</{0}>\n", this.m_Name));
			}
		}

		private XTSimpleXMLSection GetSubSection(string sname)
		{
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
			{
				if (sname == sect.m_Name)
					return sect;
			}
			return null;
		}

		// ----------------------------------------------------------------
		// public
		// ----------------------------------------------------------------
		public void AddSubSect(XTSimpleXMLSection sect)
		{
			this.m_SubSects.Add(sect);
		}

		// -------------------------------------------------------
		public string[] Keys()
		{
			string[] keys = new string[this.m_SubSects.Count];
			for (int i = 0; i < this.m_SubSects.Count; ++i)
			{
				keys[i] = this.m_SubSects[i].m_Name;
			}
			return keys;
		}

		public XTSimpleXMLSection[] Values()
		{
			XTSimpleXMLSection[] sects = new XTSimpleXMLSection[this.m_SubSects.Count];
			for (int i = 0; i < this.m_SubSects.Count; ++i)
			{
				sects[i] = this.m_SubSects[i];
			}
			return sects;
		}

		public XTSimpleXMLSection[] Values(string tagName)
		{
			XTSimpleXMLSection[] sects = new XTSimpleXMLSection[this.m_SubSects.Count];
			for (int i = 0; i < this.m_SubSects.Count; ++i)
			{
				if (this.m_SubSects[i].m_Name == tagName)
					sects[i] = this.m_SubSects[i];
			}
			return sects;
		}

		public Dictionary<string, XTSimpleXMLSection> Items()
		{
			Dictionary<string, XTSimpleXMLSection> sects = new
				Dictionary<string, XTSimpleXMLSection>();
			XTSimpleXMLSection sect;
			for (int i = 0; i < this.m_SubSects.Count; ++i)
			{
				sect = this.m_SubSects[i];
				sects[sect.m_Name] = sect;
			}
			return sects;
		}

		// ---------------------------------------------
		public string ReadString(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return "";
			return sect.AsString;
		}

		public int ReadInt(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return 0;
			return ToInt(sect.m_Value);
		}

		public long ReadLong(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return 0L;
			return ToLong(sect.m_Value);
		}

		public float ReadFloat(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return 0.0f;
			return ToFloat(sect.m_Value);
		}

		public double ReadDouble(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return 0.0;
			return ToDouble(sect.m_Value);
		}

		public bool ReadBool(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return false;
			return ToBool(sect.m_Value);
		}

		public float[] ReadVector2(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return new float[] { 0.0f, 0.0f };
			return ToVector(sect.m_Value, 2);
		}

		public float[] ReadVector3(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return new float[] { 0.0f, 0.0f, 0.0f };
			return ToVector(sect.m_Value, 3);
		}

		public float[] ReadVector4(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) return new float[] { 0.0f, 0.0f, 0.0f, 0.0f };
			return ToVector(sect.m_Value, 4);
		}

		// ---------------------------------------------
		public List<string> ReadStrings(string sname)
		{
			List<string> strs = new List<string>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					strs.Add(sect.AsString);
			return strs;
		}

		public List<int> ReadInts(string sname)
		{
			List<int> ints = new List<int>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					ints.Add(ToInt(sect.m_Value));
			return ints;
		}

		public List<long> ReadLongs(string sname)
		{
			List<long> longs = new List<long>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					longs.Add(ToLong(sect.m_Value));
			return longs;
		}

		public List<float> ReadFloats(string sname)
		{
			List<float> floats = new List<float>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					floats.Add(ToFloat(sect.m_Value));
			return floats;
		}

		public List<double> ReadDoubles(string sname)
		{
			List<double> doubles = new List<double>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					doubles.Add(ToDouble(sect.m_Value));
			return doubles;
		}

		public List<bool> ReadBools(string sname)
		{
			List<bool> bools = new List<bool>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					bools.Add(ToBool(sect.m_Value));
			return bools;
		}

		public List<float[]> ReadVector2s(string sname)
		{
			List<float[]> v2s = new List<float[]>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					v2s.Add(ToVector(sect.m_Value, 2));
			return v2s;
		}

		public List<float[]> ReadVector3s(string sname)
		{
			List<float[]> v3s = new List<float[]>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					v3s.Add(ToVector(sect.m_Value, 3));
			return v3s;
		}

		public List<float[]> ReadVector4s(string sname)
		{
			List<float[]> v4s = new List<float[]>();
			foreach (XTSimpleXMLSection sect in this.m_SubSects)
				if (sname == sect.m_Name)
					v4s.Add(ToVector(sect.m_Value, 4));
			return v4s;
		}

		// ------------------------------------------------
		public XTSimpleXMLSection WriteString(string sname, string value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsString = value;
			return sect;
		}

		public XTSimpleXMLSection WriteInt(string sname, int value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsInt = value;
			return sect;
		}

		public XTSimpleXMLSection WriteLong(string sname, long value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsLong = value;
			return sect;
		}

		public XTSimpleXMLSection WriteFloat(string sname, float value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsFloat = value;
			return sect;
		}

		public XTSimpleXMLSection WriteBool(string sname, bool value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsBool = value;
			return sect;
		}

		public XTSimpleXMLSection WriteVector2(string sname, float[] value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsVector2 = value;
			return sect;
		}

		public XTSimpleXMLSection WriteVector3(string sname, float[] value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsVector3 = value;
			return sect;
		}

		public XTSimpleXMLSection WriteVector4(string sname, float[] value)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect == null) sect = this.CreateSection(sname);
			sect.AsVector4 = value;
			return sect;
		}

		// ---------------------------------------------
		public void WriteStrings(string sname, List<string> values)
		{
			XTSimpleXMLSection sect;
			foreach (string value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsString = value;
			}
		}

		public void WriteInts(string sname, List<int> values)
		{
			XTSimpleXMLSection sect;
			foreach (int value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsInt = value;
			}
		}

		public void WriteLongs(string sname, List<long> values)
		{
			XTSimpleXMLSection sect;
			foreach (long value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsLong = value;
			}
		}

		public void WriteFloats(string sname, List<float> values)
		{
			XTSimpleXMLSection sect;
			foreach (float value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsFloat = value;
			}
		}

		public void WriteDoubles(string sname, List<double> values)
		{
			XTSimpleXMLSection sect;
			foreach (float value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsDouble = value;
			}
		}

		public void WriteBools(string sname, List<bool> values)
		{
			XTSimpleXMLSection sect;
			foreach (bool value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsBool = value;
			}
		}

		public void WriteVector2s(string sname, List<float[]> values)
		{
			XTSimpleXMLSection sect;
			foreach (float[] value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsVector2 = value;
			}
		}

		public void WriteVector3s(string sname, List<float[]> values)
		{
			XTSimpleXMLSection sect;
			foreach (float[] value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsVector3 = value;
			}
		}

		public void WriteVector4s(string sname, List<float[]> values)
		{
			XTSimpleXMLSection sect;
			foreach (float[] value in values)
			{
				sect = this.CreateSection(sname);
				sect.AsVector4 = value;
			}
		}

		// ------------------------------------------------
		/// <summary>
		/// 遍历所有子孙 section，每经过一个 secion，sfilter 都会被调用
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fnFilter">	sfilter 必需包含两个参数：
		/// ① section    表示当前遍历到的 section
		///	② out value  要传出的结果
		/// 如果 fnFilter 返回 0，则 section 被忽略
		/// 如果 fnFilter 返回 -1，则退出遍历
		/// 如果 fnFilter 返回 1，则将 fnFilter 中的第二个参数添加到结果列表
		/// </param> 函数返回一个 List，表示遍历后所得的结果		
		/// <returns> List<T> </returns>
		public List<T> TravelGets<T>(XTSimpleXMLValueFilter<T> fnFilter)
		{
			List<T> values = new List<T>();
			Queue<XTSimpleXMLSection> queue = new Queue<XTSimpleXMLSection>();
			queue.Enqueue(this);
			XTSimpleXMLSection sect;
			T value;
			int ret;
			while (queue.Count > 0)
			{
				sect = queue.Dequeue();
				ret = fnFilter(sect, out value);
				if (ret < 0)
					break;
				else if (ret > 0)
					values.Add(value);
				foreach (XTSimpleXMLSection s in sect.m_SubSects)
					queue.Enqueue(s);
			}
			return values;
		}

		public XTSimpleXMLSection CreateSection(string sname)
		{
			if (RePtns.ReptnErrTagName.Match(sname).Success)
				throw new ErrorTagNameXMLException(sname);
			XTSimpleXMLSection sect = new XTSimpleXMLSection(sname, this.m_Layer + 1);
			this.m_SubSects.Add(sect);
			return sect;
		}

		public void DeleteSection(string sname)
		{
			XTSimpleXMLSection sect = this.GetSubSection(sname);
			if (sect != null)
				this.m_SubSects.Remove(sect);
		}

		public void Save()
		{
			if (this.m_FileName == "")
				throw new IOException("save fail. only file section can be saved!");
			StreamWriter writer = new StreamWriter(this.m_FileName);
			try
			{
				this.WriteXMLCode(writer);
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				writer.Close();
			}

		}
	}
}
