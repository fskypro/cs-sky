// ------------------------------------------------------------------
// Description : 键值对型 JSON 数据
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using InnerItem = System.Collections.Generic.KeyValuePair<
	XTreme.XTJson.XTJsonKeyable, XTreme.XTJson.XTJsonData>;
using InnerDict = System.Collections.Generic.Dictionary<
	XTreme.XTJson.XTJsonKeyable, XTreme.XTJson.XTJsonData>;

namespace XTreme.XTJson
{
	public class XTJsonDict : XTJsonData, IEnumerable
	{
		public override XTJsonType Type { get { return XTJsonType.Dict; } }

		private InnerDict m_datas;

		#region 内部调用构造函数
		internal XTJsonDict(XTJsonDict dict)
		{
			if (dict == null)
				this.m_datas = new InnerDict();
			else
				this.m_datas = dict.m_datas;
		}

		internal XTJsonDict(InnerDict datas)
		{
			if (datas == null)
				this.m_datas = new InnerDict();
			else
				this.m_datas = datas;
		}

		#endregion

		#region 外部调用构造函数
		public XTJsonDict()
		{
			this.m_datas = new InnerDict();
		}

		public XTJsonDict(IDictionary<XTJsonKeyable, XTJsonData> datas)
		{
			this.m_datas = new InnerDict();
			if (datas != null)
			{
				foreach (InnerItem item in datas)
				{
					if (item.Key == null)
						throw new XTJsonKeyNullException();
					if (item.Value == null)
						this.m_datas.Add(item.Key, XTJsonNone.Inst);
					else
						this.m_datas.Add(item.Key, item.Value);
				}
			}
		}

		#endregion

		public override object ToObject()
		{
			return this.m_datas;
		}

		#region 与 Dictionary 互换（复制全部元素，效率低）
		// 把 XTJsonDict 显式转换为 Dictionary<XTJsonKeyable, XTJsonData>
		public static explicit operator InnerDict(XTJsonDict data)
		{
			return new InnerDict(data.m_datas);
		}

		// 把 Dictionary<XTJsonKeyable, XTJsonData> 转换为 XTJsonDict
		public static implicit operator XTJsonDict(InnerDict datas)
		{
			return new XTJsonDict(datas);
		}

		#endregion

		// ----------------------------------------------------------
		// 模拟 Dictionary
		// ----------------------------------------------------------
		#region 属性

		public XTJsonData this[XTJsonKeyable key]
		{
			get 
			{
				return this.m_datas[key];
			}
			set 
			{
				if (key == null)
					throw new XTJsonKeyNullException();
				if (value == null)
					this.m_datas.Add(key, XTJsonNone.Inst);
				else
					this.m_datas.Add(key, value);
			}
		}

		public List<XTJsonKeyable> Keys
		{
			get { return new List<XTJsonKeyable>(this.m_datas.Keys); }
		}

		public List<XTJsonData> Values
		{
			get { return new List<XTJsonData>(this.m_datas.Values); }
		}

		public int Count
		{
			get { return this.m_datas.Count; }
		}

		#endregion

		#region 方法

		public IEnumerator GetEnumerator()
		{
			return this.m_datas.GetEnumerator();
		}

		public bool ContainsKey(XTJsonKeyable key)
		{
			return this.m_datas.ContainsKey(key);
		}

		public void Add(XTJsonKeyable key, XTJsonData value)
		{
			if (key == null)
				throw new XTJsonKeyNullException();
			if (value == null)
				this.m_datas.Add(key, XTJsonNone.Inst);
			else
				this.m_datas.Add(key, value);
		}

		public bool Remove(XTJsonKeyable key)
		{
			return this.m_datas.Remove(key);
		}

		public bool TryGetValue(XTJsonKeyable key, out XTJsonData value)
		{
			return this.m_datas.TryGetValue(key, out value);
		}

		public void Clear()
		{
			this.m_datas.Clear();
		}

		// -------------------------------------------
		public IEnumerable<XTJsonKeyable> IterKeys()
		{
			foreach (XTJsonKeyable key in this.m_datas.Keys)
			{
				yield return key;
			}
		}

		public IEnumerable<XTJsonData> IterValues()
		{
			foreach (XTJsonData value in this.m_datas.Values)
			{
				yield return value;
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("{0}`2[{1}, {2}]", base.ToString(), 
				typeof(XTJsonKeyable).ToString(),
				typeof(XTJsonData).ToString());
		}

		// ----------------------------------------------------------
		// 保存为 JSON
		// ----------------------------------------------------------
		public XTJsonRoot SaveAs(string path, uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			XTJson.Write(path, this, warps, doc, isCache);
			if (isCache)
				return XTJson.Open(path, doc==null, false);
			return null;
		}

		public XTJsonRoot SaveAs(string path, Encoding enc, uint warps = 1, List<XTJsonComment> doc = null, bool isCache = true)
		{
			XTJson.Write(path, this, enc, warps, doc, isCache);
			if (isCache)
				return XTJson.Open(path, doc == null, false);
			return null;
		}
	}
}
