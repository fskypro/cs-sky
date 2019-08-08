// ------------------------------------------------------------------
// Description : 列表型 JSON 数据
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTreme.XTJson
{
	public class XTJsonList : XTJsonData, IEnumerable
	{
		public override XTJsonType Type { get { return XTJsonType.List; } }

		private List<XTJsonData> m_datas;

		#region 内部调用构造函数

		internal XTJsonList(List<XTJsonData> datas)
		{
			if (datas == null)
				this.m_datas = new List<XTJsonData>();
			else
				this.m_datas = datas;
		}

		#endregion

		#region 外部调用构造函数
		public XTJsonList()
		{
			this.m_datas = new List<XTJsonData>();
		}

		public XTJsonList(IEnumerable<XTJsonData> datas)
		{
			this.m_datas = new List<XTJsonData>();
			if (datas != null)
			{
				foreach (XTJsonData jdata in datas)
				{
					if (jdata == null)
						this.m_datas.Add(XTJsonNone.Inst);
					else
						this.m_datas.Add(jdata);
				}
			}
		}

		#endregion

		public override object ToObject()
		{
			return this.m_datas;
		}

		#region 与 C# 列表互换
		// 把 XTJsonList 显式转换为 List<XTJsonData>
		public static explicit operator List<XTJsonData>(XTJsonList jdata)
		{
			return new List<XTJsonData>(jdata.m_datas);
		}

		// 把 List<XTJsonData> 隐式转换为 XTJsonList
		public static implicit operator XTJsonList(List<XTJsonData> datas)
		{
			return new XTJsonList(datas);
		}

		// 把 XTJsonList 隐式转换为 XTJsonData[]
		public static explicit operator XTJsonData[](XTJsonList jdata)
		{
 			XTJsonData[] datas = new XTJsonData[jdata.m_datas.Count];
			jdata.m_datas.CopyTo(datas);
			return datas;
		}

		// 把 XTJsonData[] 显式转换为 XTJsonList
		public static implicit operator XTJsonList(XTJsonData[] datas)
		{
			return new XTJsonList(datas);
		}
		#endregion

		// -------------------------------------------
		#region 模拟 List

		public int Count
		{
			get { return this.m_datas.Count; }
		}

		public XTJsonData this[int index]
		{
			get 
			{ 
				return this.m_datas[index]; 
			}
			set
			{
				if (value == null)
					this.m_datas[index] = XTJsonNone.Inst;
				else
					this.m_datas[index] = value;
			}
		}

		// -------------------------------------------
		public IEnumerator GetEnumerator()
		{
			return this.m_datas.GetEnumerator();
		}

		public void Add(XTJsonData item)
		{
			if (item == null)
				this.m_datas.Add(XTJsonNone.Inst);
			else
				this.m_datas.Add(item);
		}

		public bool Contains(XTJsonData item)
		{
			if (item == null) item = XTJsonNone.Inst;
			return this.m_datas.Contains(item);
		}

		public void Clear()
		{
			this.m_datas.Clear();
		}

		public int IndexOf(XTJsonData item)
		{
			if (item == null) item = XTJsonNone.Inst;
			return this.m_datas.IndexOf(item);
		}

		public void Insert(int index, XTJsonData item)
		{
			if (item == null) item = XTJsonNone.Inst;
			this.m_datas.Insert(index, item);
		}

		public bool Remove(XTJsonData item)
		{
			if (item == null) item = XTJsonNone.Inst;
			return this.m_datas.Remove(item);
		}

		public void RemoveAt(int index)
		{
			this.m_datas.RemoveAt(index);
		}

		public void CopyTo(XTJsonData[] datas)
		{
			this.m_datas.CopyTo(datas);
		}

		public void Reverse()
		{
			this.m_datas.Reverse();
		}

		#endregion

		public override string ToString()
		{
			return string.Format("{0}`1[{1}]", base.ToString(), typeof(XTJsonData).ToString());
		}
	}
}
