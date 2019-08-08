// ------------------------------------------------------------------
// Description : Xtreme 异常
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date	       : 2014.06.21
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace XTreme
{
	 public class XTException : Exception
	 {
	 	 public XTException()
	 	 { }

	 	 public XTException(string msg)
	 	 	 : base(msg)
	 	 { }

	 	 public XTException(string msg, Exception innerException)
	 	 	 : base(msg, innerException)
	 	 { }

		 public XTException(SerializationInfo info, StreamingContext contex)
	 	 	 : base(info, contex)
	 	 { }
	 }
}