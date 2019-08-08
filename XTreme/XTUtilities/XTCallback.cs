// ------------------------------------------------------------------
// Description : 自定义一组通用允许带或不带额外参数的回调
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.08.28
// Histories   :
// ------------------------------------------------------------------

using System;

namespace XTreme.XTUtilities
{
	// --------------------------------------------------------------
	// 没有参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase
	{
		public abstract void Call();
		public static implicit operator XTCBWrapperBase(XTCBWrapper.Callback cb)
		{ return new XTCBWrapper(cb); }
	}

	public class XTCBWrapper : XTCBWrapperBase
	{
		public delegate void Callback();
		private Callback m_cb;
		public XTCBWrapper(Callback cb) { this.m_cb = cb; }
		public override void Call(){ this.m_cb(); }
	}

	public class XTParamsCBWrapper : XTCBWrapperBase
	{
		public delegate void Callback(params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCBWrapper(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call() { this.m_cb(this.m_extras); }
	}

	// --------------------------------------------------------------
	// 一个参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase1<A1>
	{
		public abstract void Call(A1 a1);
		public static implicit operator XTCBWrapperBase1<A1>(XTCBWrapper1<A1>.Callback cb)
		{ return new XTCBWrapper1<A1>(cb); }
	}

	public class XTCBWrapper1<A1> : XTCBWrapperBase1<A1>
	{
		public delegate void Callback(A1 v);
		private Callback m_cb;
		public XTCBWrapper1(Callback cb) { this.m_cb = cb; }
		public override void Call(A1 a1) { this.m_cb(a1); }
	}

	public class XTParamsCBWrapper1<A1> : XTCBWrapperBase1<A1>
	{
		public delegate void Callback(A1 v, params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCBWrapper1(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call(A1 a1) { this.m_cb(a1, this.m_extras); }
	}

	// --------------------------------------------------------------
	// 两个参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase2<A1, A2>
	{
		public abstract void Call(A1 a1, A2 a2);
		public static implicit operator XTCBWrapperBase2<A1, A2>(XTCBWrapper2<A1, A2>.Callback cb)
		{ return new XTCBWrapper2<A1, A2>(cb); }
	}

	public class XTCBWrapper2<A1, A2> : XTCBWrapperBase2<A1, A2>
	{
		public delegate void Callback(A1 a1, A2 a2);
		private Callback m_cb;
		public XTCBWrapper2(Callback cb) { this.m_cb = cb; }
		public override void Call(A1 a1, A2 a2) { this.m_cb(a1, a2); }
	}

	public class XTParamsCBWrapper2<A1, A2> : XTCBWrapperBase2<A1, A2>
	{
		public delegate void Callback(A1 a1, A2 a2, params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCBWrapper2(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call(A1 a1, A2 a2) { this.m_cb(a1, a2, this.m_extras); }
	}

	// --------------------------------------------------------------
	// 三个参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase3<A1, A2, A3>
	{
		public abstract void Call(A1 a1, A2 a2, A3 a3);
		public static implicit operator XTCBWrapperBase3<A1, A2, A3>(XTCBWrapper3<A1, A2, A3>.Callback cb)
		{ return new XTCBWrapper3<A1, A2, A3>(cb); }
	}

	public class XTCBWrapper3<A1, A2, A3> : XTCBWrapperBase3<A1, A2, A3>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3);
		private Callback m_cb;
		public XTCBWrapper3(Callback cb) { this.m_cb = cb; }
		public override void Call(A1 a1, A2 a2, A3 a3) { this.m_cb(a1, a2, a3); }
	}

	public class XTParamsCBWrapper3<A1, A2, A3> : XTCBWrapperBase3<A1, A2, A3>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3, params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCBWrapper3(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call(A1 a1, A2 a2, A3 a3) { this.m_cb(a1, a2, a3, this.m_extras); }
	}

	// --------------------------------------------------------------
	// 四个参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase4<A1, A2, A3, A4>
	{
		public abstract void Call(A1 a1, A2 a2, A3 a3, A4 a4);
		public static implicit operator XTCBWrapperBase4<A1, A2, A3, A4>(XTCallback4<A1, A2, A3, A4>.Callback cb)
		{ return new XTCallback4<A1, A2, A3, A4>(cb); }
	}

	public class XTCallback4<A1, A2, A3, A4> : XTCBWrapperBase4<A1, A2, A3, A4>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3, A4 a4);
		private Callback m_cb;
		public XTCallback4(Callback cb) { this.m_cb = cb; }
		public override void Call(A1 a1, A2 a2, A3 a3, A4 a4) { this.m_cb(a1, a2, a3, a4); }
	}

	public class XTParamsCallback4<A1, A2, A3, A4> : XTCBWrapperBase4<A1, A2, A3, A4>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3, A4 a4, params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCallback4(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call(A1 a1, A2 a2, A3 a3, A4 a4) { this.m_cb(a1, a2, a3, a4, this.m_extras); }
	}

	// --------------------------------------------------------------
	// 五个参数的代理
	// --------------------------------------------------------------
	public abstract class XTCBWrapperBase5<A1, A2, A3, A4, A5>
	{
		public abstract void Call(A1 a1, A2 a2, A3 a3, A4 a4, A5 a5);
		public static implicit operator XTCBWrapperBase5<A1, A2, A3, A4, A5>(XTCallback5<A1, A2, A3, A4, A5>.Callback cb)
		{ return new XTCallback5<A1, A2, A3, A4, A5>(cb); }
	}

	public class XTCallback5<A1, A2, A3, A4, A5> : XTCBWrapperBase5<A1, A2, A3, A4, A5>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3, A4 a4, A5 a5);
		private Callback m_cb;
		public XTCallback5(Callback cb) { this.m_cb = cb; }
		public override void Call(A1 a1, A2 a2, A3 a3, A4 a4, A5 a5) { this.m_cb(a1, a2, a3, a4, a5); }
	}

	public class XTParamsCallback5<A1, A2, A3, A4, A5> : XTCBWrapperBase5<A1, A2, A3, A4, A5>
	{
		public delegate void Callback(A1 a1, A2 a2, A3 a3, A4 a4, A5 a5, params object[] extras);
		private Callback m_cb;
		private object[] m_extras;
		public XTParamsCallback5(Callback cb, params object[] extras)
		{ this.m_cb = cb; this.m_extras = extras; }
		public override void Call(A1 a1, A2 a2, A3 a3, A4 a4, A5 a5) { this.m_cb(a1, a2, a3, a4, a5, this.m_extras); }
	}
}
