// ------------------------------------------------------------------
// Description : 全局事件管理器
// Author      : hyw
// Date        : 2014.11.18
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace XTreme.XTModes
{
	// --------------------------------------------------------------
	// 事件代理
	// --------------------------------------------------------------
	#region XTEventDelegates

	public delegate void XTEventDelegate1();
	public delegate void XTEventDelegate2(params object[] arg);

	public class XTEventDelegates<T>
	{
		private T m_eid;
		private List<Delegate> m_edlgs;

		public XTEventDelegates(T eid)
		{
			this.m_eid = eid;
			this.m_edlgs = new List<Delegate>();
		}

		public void Add(Delegate edlg)
		{
			this.m_edlgs.Add(edlg);
		}

		public bool Remove(Delegate edlg)
		{
			if (this.m_edlgs.Contains(edlg))
			{
				this.m_edlgs.Remove(edlg);
			}
			return this.m_edlgs.Count == 0;
		}

		public void Call()
		{
			foreach (Delegate edlg in this.m_edlgs)
			{
				if (edlg.GetType() == typeof(XTEventDelegate1))
					((XTEventDelegate1)edlg)();
			}
		}

		public void Call(params object[] args)
		{
			foreach (Delegate edlg in this.m_edlgs)
			{
				if (edlg.GetType() == typeof(XTEventDelegate1))
					((XTEventDelegate1)edlg)();
				else if (edlg.GetType() == typeof(XTEventDelegate2))
					((XTEventDelegate2)edlg)(args);
			}
		}
	}

	#endregion

	// --------------------------------------------------------------
	// 事件管理器
	// --------------------------------------------------------------
	#region XTEvent
	public class XTEvent<T>
	{
		private Dictionary<T, XTEventDelegates<T>> m_events;

		private XTEvent()
		{
			this.m_events = new Dictionary<T, XTEventDelegates<T>>();
		}

		// ----------------------------------------------------------
		// public
		// ----------------------------------------------------------
		public void Regist(T eid, XTEventDelegate1 edlg)
		{
			if (!this.m_events.ContainsKey(eid))
			{
				this.m_events[eid] = new XTEventDelegates<T>(eid);
			}
			this.m_events[eid].Add(edlg);
		}

		public void Unregist(T eid, XTEventDelegate1 edlg)
		{
			if (this.m_events.ContainsKey(eid) && this.m_events[eid].Remove(edlg))
			{
				this.m_events.Remove(eid);
			}
		}

		public void Regist(T eid, XTEventDelegate2 edlg)
		{
			if (!this.m_events.ContainsKey(eid))
			{
				this.m_events[eid] = new XTEventDelegates<T>(eid);
			}
			this.m_events[eid].Add(edlg);
		}

		public void Unregist(T eid, XTEventDelegate2 edlg)
		{
			if (this.m_events.ContainsKey(eid) && this.m_events[eid].Remove(edlg))
			{
				this.m_events.Remove(eid);
			}
		}

		// -------------------------------------------
		public void Fire(T eid)
		{
			XTEventDelegates<T> edlgs;
			if (this.m_events.TryGetValue(eid, out edlgs))
			{
				edlgs.Call();
			}
		}

		public void Fire(T eid, params object[] args)
		{
			XTEventDelegates<T> edlgs;
			if (this.m_events.TryGetValue(eid, out edlgs))
			{
				edlgs.Call(args);
			}
		}
	}
	#endregion
}