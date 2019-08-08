// ------------------------------------------------------------------
// Description : 函数元素
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace XTreme.XTFormula
{
	// --------------------------------------------------------------
	// 函数元素基类
	// --------------------------------------------------------------
	internal class XTFunctionParser : XTFormulaTokenParser
	{
		// ----------------------------------------------------------
		// 函数信息
		// ----------------------------------------------------------
		#region Class FuncInfo
		abstract class BaseFuncInfo
		{
			private const string PTN_START = @"^\s*{0}\s*\(\s*";
			private string m_name;
			private readonly Regex m_reStart;
			private readonly ushort m_argCount;
			public BaseFuncInfo(string name, ushort argCount)
			{
				this.m_name = name;
				this.m_reStart = new Regex(string.Format(PTN_START, name));
				this.m_argCount = argCount;
			}
			public string Name { get { return this.m_name; } }
			public Regex ReStart { get{ return this.m_reStart; } }
			public ushort ArgCount { get{ return this.m_argCount; } }
			public virtual XTFormulaToken CreateToken(XTFormula[] formulas) { return null; }
		}

		class FuncInfo<T> : BaseFuncInfo where T : XTFuncToken, new()
		{
			public FuncInfo(string name, ushort argCount) : base(name, argCount){}
			public override XTFormulaToken CreateToken(XTFormula[] formulas)
			{
				return XTFuncToken.Create<T>(formulas);
			}
		}

		#endregion

		// ----------------------------------------------------------
		// 函数解释器
		// ----------------------------------------------------------
		private static Regex sm_reSplit;
		private static Regex sm_reEnd;
		private static BaseFuncInfo[] sm_funcInfos;

		static XTFunctionParser()
		{
			sm_reSplit = new Regex(@"^\s*,\s*");
			sm_reEnd = new Regex(@"^\s*\)\s*");
			sm_funcInfos = new BaseFuncInfo[]{
				new FuncInfo<ExSqrToken>("sqr", 2),			// 开方
				new FuncInfo<XTMaxToken>("max", 2),			// 取最大值	
				new FuncInfo<XTMinToken>("min", 2),			// 取最小值
				new FuncInfo<XTRndToken>("rnd", 2),			// 获取指定范围内随机数
				new FuncInfo<XTIntPartToken>("int", 1),		// 取整函数
				new FuncInfo<XTRoundToken>("round", 1),		// 四舍五入函数
				};
		}

		private XTFormulaToken CreateToken(XTFormulaParser parser, HashSet<string> argNames, BaseFuncInfo funcInfo)
		{
			// 函数开始
			if (parser.NextRegx(funcInfo.ReStart) == null) return null;

			// 函数参数
			XTFormula formula;
			XTFormula[] formulas = new XTFormula[funcInfo.ArgCount];
			for (int i = 0; i < funcInfo.ArgCount; ++i)
			{
				if (i > 0)
				{
					if (parser.NextRegx(sm_reSplit) == null)
						parser.RaiseFormulaException();
				}
				formula = parser.InnerParse(argNames);
				if (formula == null)
					parser.RaiseFormulaException();
				formulas[i] = formula;
			}

			// 函数结束
			if (parser.NextRegx(sm_reEnd) == null)
				parser.RaiseFormulaException();
			return funcInfo.CreateToken(formulas);
		}

		public override XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			XTFormulaToken token;
			foreach (BaseFuncInfo info in sm_funcInfos)
			{
				token = this.CreateToken(parser, argNames, info);
				if (token == null) continue;
				return token;
			}
			return null;
		}
	}
}
