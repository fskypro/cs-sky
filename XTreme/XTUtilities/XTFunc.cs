// ------------------------------------------------------------------
// Description : 自定义一组通用代理
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;

namespace XTreme.XTUtilities
{
	public delegate void XTVoidFunc();
	public delegate void XTVoidFunc<T>(T a);
	public delegate void XTVoidFunc<T1, T2>(T1 a1, T2 a2);
	public delegate void XTVoidFunc<T1, T2, T3>(T1 a1, T2 a2, T3 a3);
	public delegate void XTVoidFunc<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4);
	public delegate void XTVoidFunc<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
	public delegate void XTVoidFunc<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
	public delegate void XTVoidFunc<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);

	public delegate Ret ExRetFunc<Ret>();
	public delegate Ret ExRetFunc<Ret, T>(T a);
	public delegate Ret ExRetFunc<Ret, T1, T2>(T1 a1, T2 a2);
	public delegate Ret ExRetFunc<Ret, T1, T2, T3>(T1 a1, T2 a2, T3 a3);
	public delegate Ret ExRetFunc<Ret, T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4);
	public delegate Ret ExRetFunc<Ret, T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5);
	public delegate Ret ExRetFunc<Ret, T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6);
	public delegate Ret ExRetFunc<Ret, T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7);
}
