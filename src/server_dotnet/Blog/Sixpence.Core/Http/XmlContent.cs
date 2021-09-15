using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sixpence.Core.Http
{
	public class XmlContent : ByteArrayContent
	{
		public XmlContent(string value)
			: base(Encoding.UTF8.GetBytes(value))
		{
		}
	}
}
